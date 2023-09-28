using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Microsoft.Win32;

namespace Excel2Any
{
    public class ExcelHelper
    {
        static ExcelDataTableConfiguration tableConfig;
        public static ExcelDataSetConfiguration dataSetConfig;

        static CommonSetting _common = new CommonSetting();

        static Dictionary<Type, (int code, Type coverter, Type setting, Type saver)> map =
            new Dictionary<Type, (int code, Type coverter, Type setting, Type saver)>();

        static Dictionary<Type, Entity> entityMap = new Dictionary<Type, Entity>();
        static ExcelHelper()
        {
            tableConfig = new ExcelDataTableConfiguration();
            dataSetConfig = new ExcelDataSetConfiguration
            {
                UseColumnDataType = true,
                ConfigureDataTable = (_) => { return tableConfig; }
            };
            Collect();
        }

        public static void Init(CommonSetting common)
        {
            _common = common;
            SetAllResultsDirty();
        }
        public static IEnumerable<Type> GetSubTypesInAssemblys(Type self)
        {
            if (self.IsInterface)
                return AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(item => item.GetTypes())
                                .Where(item => item.GetInterfaces().Contains(self));
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(item => item.GetTypes())
                            .Where(item => item.IsSubclassOf(self));
        }

        /// <summary>
        /// 反射获取实体类关系
        /// </summary>
        public static void Collect()
        {
            var entitys = GetSubTypesInAssemblys(typeof(Entity));
            var sets = GetSubTypesInAssemblys(typeof(ISetting));
            var coveters = GetSubTypesInAssemblys(typeof(IConverter));
            var savers = GetSubTypesInAssemblys(typeof(IConverterSaver));

            Dictionary<Type, Type> setMap = new Dictionary<Type, Type>();
            Dictionary<Type, Type> coveterMap = new Dictionary<Type, Type>();
            Dictionary<Type, Type> saveMap = new Dictionary<Type, Type>();

            foreach (var item in sets)
            {
                if (item.IsDefined(typeof(EntityAttribute), false))
                {
                    var attr = item.GetCustomAttributes(typeof(EntityAttribute), false)[0] as EntityAttribute;
                    var entityType = attr.entityType;
                    setMap.Add(entityType, item);
                }
            }
            foreach (var item in coveters)
            {
                if (item.IsDefined(typeof(EntityAttribute), false))
                {
                    var attr = item.GetCustomAttributes(typeof(EntityAttribute), false)[0] as EntityAttribute;
                    var entityType = attr.entityType;
                    coveterMap.Add(entityType, item);
                }
            }
            foreach (var item in savers)
            {
                if (item.IsDefined(typeof(EntityAttribute), false))
                {
                    var attr = item.GetCustomAttributes(typeof(EntityAttribute), false)[0] as EntityAttribute;
                    var entityType = attr.entityType;
                    saveMap.Add(entityType, item);
                }
            }

            foreach (var item in entitys)
            {
                var attr = item.GetCustomAttributes(typeof(EntityCodeAttribute), false)[0] as EntityCodeAttribute;
                map.Add(item, (attr.code, coveterMap[item], setMap[item], saveMap[item]));
            }
        }

        /// <summary>
        /// 获取实体，不存在时创建
        /// </summary>
        /// <param name="entityType">类型</param>
        /// <returns>实例</returns>
        public static Entity GetEntity(Type entityType)
        {
            if (!entityMap.ContainsKey(entityType))
            {
                var saveType = GetSaveType(entityType);
                var setType = GetSetType(entityType);
                var converterType = GetConverterType(entityType);
                var set = Activator.CreateInstance(setType) as BaseSetting;
                var save = Activator.CreateInstance(saveType, new object[] { set });
                var converter = Activator.CreateInstance(converterType, new object[] { set });
                Entity entity = Activator.CreateInstance(entityType, new object[] { converter, save }) as Entity;
                entityMap[entityType] = entity;
            }
            return entityMap[entityType];
        }
        private static Type GetSetType(Type entityType) => map[entityType].setting;
        private static Type GetSaveType(Type entityType) => map[entityType].saver;
        private static Type GetConverterType(Type entityType) => map[entityType].coverter;
        private static int GetConverterCode(Type entityType) => map[entityType].code;
        public static IReadOnlyCollection<Type> GetAllEntityTypes()
        {
            var list = map.Keys.ToList();
            list.Sort((x, y) => { return GetConverterCode(x) - GetConverterCode(y); });
            return list;
        }

        private static Dictionary<string, RawDataDetail> history = new Dictionary<string, RawDataDetail>();
        private static Dictionary<Type, Dictionary<string, List<SheetData>>> results = new Dictionary<Type, Dictionary<string, List<SheetData>>>();
        /// <summary>
        /// 清理对应类型对应路径的缓存
        /// </summary>
        /// <param name="entityType">转换器类型</param>
        /// <param name="path">路径</param>
        public static void SetResultDirty(Type entityType, string path)
        {
            if (!results.ContainsKey(entityType)) return;
            if (results[entityType].ContainsKey(path))
            {
                results[entityType].Remove(path);
            }
        }
        /// <summary>
        /// 清除对应类型的所有缓存
        /// </summary>
        /// <param name="entityType"></param>
        public static void SetAllResultsDirty(Type entityType = null)
        {
            if (entityType == null)
                results.Clear();
            else
            {
                if (results.ContainsKey(entityType))
                {
                    results[entityType].Clear();
                }
            }
        }

        public static void SetHistoryDirty(string path)
        {
            if (!history.ContainsKey(path)) return;
            else history.Remove(path);
        }

        public static void SetAllHistoryDirty()
        {
            history.Clear();
        }

        public static List<SheetData> GetSheets(Type entityType, string path)
        {
            if (!results.ContainsKey(entityType))
            {
                results.Add(entityType, new Dictionary<string, List<SheetData>>());
            }

            if (!results[entityType].ContainsKey(path) || history[path].crc != CRC.GetFileCRC(path))
            {
                Create(entityType, path);
            }
            return results[entityType][path];
        }

        public static List<List<RowHead>> GetRowHeads(Type entityType, string path)
        {
            if (!history.ContainsKey(path))
            {
                Create(entityType, path);
            }
            return history[path].headsCollection;
        }

        private static bool CheckHistoiry(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            bool isChange;
            if (!history.ContainsKey(fileInfo.FullName))
            {
                isChange = true;
            }
            else
            {
                isChange = history[path].crc != CRC.GetFileCRC(path);
            }

            return isChange;
        }
        private static void Create(Type entityType, string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            var entity = GetEntity(entityType);

            if (CheckHistoiry(path) || history[fileInfo.FullName].data == null)
            {
                try
                {
                    using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var data = reader.AsDataSet(dataSetConfig);
                            var headsCollection = new List<List<RowHead>>();
                            //获取一个保存Key的字典，先判断有没有文件夹和文件，如果没有就创建
                            var keys = ReadKey(path);
                            var result = CreateRawData(data, keys, out headsCollection);
                            result.DataSetName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));

                            RawDataDetail hisData = new RawDataDetail(CRC.GetFileCRC(path), result, headsCollection);
                            history[fileInfo.FullName] = hisData;
                        }
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }

            }

            results[entityType][fileInfo.FullName] = entity.Convert(history[fileInfo.FullName]);

        }
        private static DataSet CreateRawData(DataSet dataSet, HashSet<string> keys, out List<List<RowHead>> headsCollection)
        {
            var result = new DataSet();
            headsCollection = new List<List<RowHead>>();
            for (int tableIndex = 0; tableIndex < dataSet.Tables.Count; tableIndex++)
            {
                DataTable sheet = dataSet.Tables[tableIndex];
                if (sheet.Rows.Count <= 0) continue;
                //获取sheet名，判断是否需要排除
                var tabelName = _common.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;
                if (_common.excludeSheet && tabelName.Contains(_common.excludeSheetString)) continue;
                if (result.Tables.Contains(tabelName)) continue;


                //保存表头的索引
                var rowHeads = new List<RowHead>();
                DataTable resultTable = new DataTable();
                //判断是否排除第一列
                int startCol = _common.excludeFirstCol ? 1 : 0;
                resultTable.TableName = tabelName;

                //列表头索引和名字获取
                for (int i = startCol; i < sheet.Columns.Count; i++)
                {

                    if (_common.FieldRowNum < sheet.Rows.Count)
                    {
                        //排除属性名为空的列
                        var fieldName = sheet.Rows[_common.FieldRowNum][i].ToString();
                        if (string.IsNullOrWhiteSpace(fieldName)) continue;

                        //检查是否存在相同名字的Col
                        if (resultTable.Columns.Contains(fieldName)) continue;

                        Type fieldType = typeof(object);
                        string fieldTypeName = "object";
                        if (_common.TypeRowNum < sheet.Rows.Count)
                        {
                            fieldTypeName = sheet.Rows[_common.TypeRowNum][i].ToString().Trim();
                            fieldType = FieldTypeUtil.GetType(fieldTypeName.ToLower());
                            if (fieldType != typeof(object))
                            {
                                fieldTypeName = fieldTypeName.ToLower();
                            }
                        }


                        var fieldComment = "";
                        if (_common.CommentRowNum < sheet.Rows.Count)
                        {
                            fieldComment = sheet.Rows[_common.CommentRowNum][i].ToString();
                        }

                        rowHeads.Add(new RowHead(fieldName, i, fieldTypeName, fieldType, fieldComment, keys.Contains(fieldName)));

                        resultTable.Columns.Add(fieldName, fieldType);
                    }

                }

                //表头都没有的Sheet直接跳过
                if (rowHeads.Count == 0) continue;

                if (_common.StartRowNum >= 0)
                {
                    for (int i = _common.StartRowNum; i < sheet.Rows.Count; i++)
                    {
                        DataRow row = resultTable.NewRow();
                        for (int j = 0; j < rowHeads.Count; j++)
                        {
                            object value = sheet.Rows[i][rowHeads[j].index];

                            //增加判断数组
                            if (rowHeads[j].type.IsArray)
                            {
                                value = GetArrayByString(value.ToString(), rowHeads[j].type.GetArrayRank());
                            }
                            row[j] = value;
                        }
                        resultTable.Rows.Add(row);
                    }
                }

                headsCollection.Add(rowHeads);
                result.Tables.Add(resultTable);
            }
            return result;
        }

        /// <summary>
        /// 根据类型和文件路径保存文件
        /// </summary>
        /// <param name="entityType">转换实体类型</param>
        /// <param name="path"></param>
        public static void Save(Type entityType, string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            var filename = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."));
            var entity = GetEntity(entityType);

            entity.Save2File(GetSheets(entityType, path), filename);
        }
        private static object GetArrayByString(object value, int rank)
        {
            var str = value as string;
            if (rank == 1)
            {
                str = str.Replace("[", "").Replace("]", "");

                if (string.IsNullOrWhiteSpace(str)) return null;
                return str.Split(',');
            }
            else if (rank == 2)
            {
                str = str.Replace("[[", "").Replace("]]", "").Replace("],[", "[");

                if (string.IsNullOrWhiteSpace(str)) return null;
                var rows = str.Split('[');

                int rowCount = rows.Length;
                int colCount = rows[0].Split(',').Length;

                string[,] myArray = new string[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    try
                    {
                        string[] elements = rows[i].Split(',');
                        for (int j = 0; j < colCount; j++)
                        {
                            myArray[i, j] = elements[j];
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                return myArray;
            }
            else return value;
        }

        private static HashSet<string> ReadKey(string path)
        {
            var keys = new HashSet<string>();
            var keyDir = Path.GetDirectoryName(path) + "/.keys";
            var fileName = Path.GetFileNameWithoutExtension(path);
            if (Directory.Exists(keyDir))
            {
                var keyFileDir = Path.Combine(keyDir, fileName);
                if (File.Exists(keyFileDir))
                {
                    var str = File.ReadAllText(keyFileDir);
                    try
                    {
                        keys = Newtonsoft.Json.JsonConvert.DeserializeObject<HashSet<string>>(str);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (keys is null)
            {

                keys = new HashSet<string>();
            }
            return keys;
        }
    }
}
