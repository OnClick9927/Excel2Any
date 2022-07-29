using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other.Winform
{
    public class SettingHelper
    {
        public static List<FieldInfo> GetFields(Type type)
        {
            List<FieldInfo> fs = new List<FieldInfo>();
            if (type != typeof(System.Object))
            {
                var _fs = type.GetFields();
                fs.InsertRange(0, _fs);
                //type = type.BaseType;
            }
            return fs;
        }

        static string rootPath = "";

        private static string GetDefaultSettingPath(Type type)
        {
            return Path.Combine(rootPath, $"Settings/config.{type.Name}");
        }
        public static ISetting GetSetting(Type entityType, string path = "")
        {
            var settingType = UIEntityHelper.GetUIEntity(entityType).setting.GetType();
            if (string.IsNullOrEmpty(path))
            {
                path = GetDefaultSettingPath(settingType);
            }
            if (!File.Exists(path))
            {
                return default;
                //SaveSetting(Activator.CreateInstance(settingType) as ISetting, path);
            }
            return GetSettingBySettingType(settingType, path);
        }

        private static ISetting GetSettingBySettingType(Type settingType, string path)
        {
            if (!File.Exists(path))
            {
                return default;
            }
            var str = File.ReadAllText(path);
            var setting = JsonConvert.DeserializeObject(str, settingType) as ISetting;

            return setting;
        }

        public static void SaveSetting(ISetting set, string path = "")
        {
            string str = JsonConvert.SerializeObject(set);
            if (string.IsNullOrEmpty(path))
            {
                var type = set.GetType();
                path = GetDefaultSettingPath(type);
            }
            File.WriteAllText(path, str);
        }

        public static ISetting GetFormSetting()
        {
            Type type = typeof(FormSetting);
            var ISetting = GetSettingBySettingType(type, GetDefaultSettingPath(type));
            if (ISetting == null)
            {
                ISetting = new FormSetting();
                SaveFormSetting(ISetting);
            }
            return ISetting;
        }

        private static void SaveFormSetting(ISetting set)
        {
            SaveSetting(set, GetDefaultSettingPath(typeof(FormSetting)));
        }
    }
}
