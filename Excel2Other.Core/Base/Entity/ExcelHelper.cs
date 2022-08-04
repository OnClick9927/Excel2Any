using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace Excel2Other
{
    public class ExcelHelper
    {
        static ExcelDataTableConfiguration tableConfig;
        public static ExcelDataSetConfiguration dataSetConfig;
        /// <summary>
        /// map 保存 code、Convert类型、Setting类型、Saver类型
        /// </summary>
        static Dictionary<Type, Tuple<int, Type, Type, Type>> map = new Dictionary<Type, Tuple<int, Type, Type, Type>>();
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
                map.Add(item, Tuple.Create(attr.code, coveterMap[item], setMap[item], saveMap[item]));
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
                var set = Activator.CreateInstance(setType);
                var save = Activator.CreateInstance(saveType, new object[] { set });
                var converter = Activator.CreateInstance(converterType, new object[] { set });
                Entity entity = Activator.CreateInstance(entityType, new object[] { converter, save }) as Entity;
                entityMap[entityType] = entity;
            }
            return entityMap[entityType];
        }

        private static Type GetSetType(Type entityType)
        {
            var tuple = map[entityType];
            var setType = tuple.Item3;
            return setType;
        }
        private static Type GetSaveType(Type entityType)
        {
            var tuple = map[entityType];
            var saveType = tuple.Item4;
            return saveType;
        }
        private static Type GetConverterType(Type entityType)
        {
            var tuple = map[entityType];
            var converterType = tuple.Item2;
            return converterType;
        }
        private static int GetConverterCode(Type entityType)
        {
            var tuple = map[entityType];
            var code = tuple.Item1;
            return code;
        }
        public static IReadOnlyCollection<Type> GetAllEntityTypes()
        {
            var list = map.Keys.ToList();
            list.Sort((x, y) => { return GetConverterCode(x) - GetConverterCode(y); });
            return list;
        }

        private static Dictionary<string, HistoryData> history = new Dictionary<string, HistoryData>();
        private static Dictionary<Type, Dictionary<string, List<SheetData>>> results = new Dictionary<Type, Dictionary<string, List<SheetData>>>();
        /// <summary>
        /// 清理对应类型对应路径的缓存
        /// </summary>
        /// <param name="entityType">转换器类型</param>
        /// <param name="path">路径</param>
        public static void SetDirty(Type entityType, string path)
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
        public static void SetAllDirty(Type entityType)
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
        public static List<SheetData> GetSheets(Type entityType, string path)
        {
            if (!results.ContainsKey(entityType))
            {
                results.Add(entityType, new Dictionary<string, List<SheetData>>());
            }

            FileInfo fileInfo = new FileInfo(path);

            if (!results[entityType].ContainsKey(path) || history[path].lastWriteTime != fileInfo.LastWriteTime)
            {
                Create(entityType, path);
            }
            return results[entityType][path];
        }
        private static bool CheckHistoiry(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            DateTime lastWriteTime = fileInfo.LastWriteTime;
            var isChange = false;
            if (!history.ContainsKey(fileInfo.FullName))
            {
                isChange = true;
            }
            else
            {
                isChange = history[path].lastWriteTime != lastWriteTime;
            }

            return isChange;
        }
        private static void Create(Type entityType, string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            var entity = GetEntity(entityType);

            if (CheckHistoiry(path) || history[fileInfo.FullName].data == null)
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(ExcelHelper.dataSetConfig);
                        result.DataSetName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));
                        HistoryData hisData = new HistoryData(fileInfo.LastWriteTime, result);
                        history[fileInfo.FullName] = hisData;
                    }
                }
            }

            results[entityType][fileInfo.FullName] = entity.Convert(history[fileInfo.FullName].data);

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
    }
}
