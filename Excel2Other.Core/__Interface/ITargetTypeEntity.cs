using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Excel2Other.Core.__Interface
{
    public class DataMap
    {
        private static Dictionary<Type, Dictionary<string, List<SheetData>>> results = new Dictionary<Type, Dictionary<string, List<SheetData>>>();
        public static void SetDirty(Type entityType, string path)
        {
            if (!results.ContainsKey(entityType)) return;
            if (results[entityType].ContainsKey(path))
            {
                results[entityType].Remove(path);
            }
        }
        public static void SetAllDirty(Type entityType)
        {
            if (entityType == null)
                results.Clear();
            else
                results[entityType].Clear();
        }
        public static List<SheetData> GetSheets(Type entityType, string path)
        {
            if (!results.ContainsKey(entityType))
            {
                results.Add(entityType, new Dictionary<string, List<SheetData>>());
            }
            if (!results[entityType].ContainsKey(path))
            {
                Create(entityType, path);
            }
            return results[entityType][path];
        }
        private static void Create(Type entityType, string path)
        {
            var entity = Entity.GetEntity(entityType);
            FileInfo fileInfo = new FileInfo(path);

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(ExcelReader.dataSetConfig);
                    result.DataSetName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));
                    entity.Convert(result);
                }
            }
        }
    }
    public class Entity
    {
        static Dictionary<Type, Tuple<int, Type, Type, Type>> map = new Dictionary<Type, Tuple<int, Type, Type, Type>>();
        static Dictionary<Type, Entity> entityMap = new Dictionary<Type, Entity>();
        static Entity()
        {
            Collect();
        }
        private static IEnumerable<Type> GetSubTypesInAssemblys(Type self)
        {
            if (self.IsInterface)
                return AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(item => item.GetTypes())
                                .Where(item => item.GetInterfaces().Contains(self));
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(item => item.GetTypes())
                            .Where(item => item.IsSubclassOf(self));
        }
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
                map.Add(item, Tuple.Create(attr.code,coveterMap[item], setMap[item], saveMap[item]));
            }
        }


        public static Entity GetEntity(Type entityType)
        {
            if (!entityMap.ContainsKey(entityType))
            {
                var saveType = GetSaveType(entityType);
                Entity entity = Activator.CreateInstance(entityType, new object[] { Activator.CreateInstance(saveType) }) as Entity;
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
        private static IEnumerable<Type> GetAllEntityTypes()
        {
            return map.Keys;
        }

        private IConverter _converter;
        private ISetting _set;
        private IConverterSaver _save;
        public IConverter converter => _converter;
        public ISetting set => _set;
        public IConverterSaver save => _save;
        public Entity(IConverterSaver save)
        {
            _save = save;
        }
        public void SetSetting(ISetting set)
        {
            if (converter == null)
            {
                var converterType = GetConverterType(GetType());
                _converter = Activator.CreateInstance(converterType, new object[] { set }) as IConverter;
            }
            else
                converter.SetSetting(set);
        }
        public ISetting SetSetting()
        {
            return converter?.GetSetting();
        }
        public List<SheetData> Convert(DataSet data)
        {
            return converter?.Convert(data);
        }
        public string Save2String(List<SheetData> sheets)
        {
            return save.Save2String(sheets);
        }
        public void Save2File(string path, List<SheetData> sheets)
        {
            save.Save2File(path, sheets);
        }
    }
}
