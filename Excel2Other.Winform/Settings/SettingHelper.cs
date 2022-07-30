using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        static readonly string  rootPath = "";

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

        public static void SaveSetting(Type entityType, bool isSelect = false)
        {
            ISetting setting = UIEntityHelper.GetUIEntity(entityType).setting;
            string savePath;
            if (isSelect)
            {
                SaveFileDialog save = new SaveFileDialog();
                var settingExtension = UIEntityHelper.GetSettingExtension(entityType);
                save.Filter = $"配置文件|*.{settingExtension}";     //设置保存类型
                save.Title = $"请设置{settingExtension}的保存位置和文件名";   //对话框标题
                save.FileName = "config";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    savePath = save.FileName;
                    SaveSetting(setting, savePath);
                }
                else
                {
                    return;
                }
            }
            else
            {
                SaveSetting(setting);
            }
            ExcelHelper.SetAllDirty(entityType);
        }

        public static void LoadSetting(Type settingType, bool isSelect = false)
        {
            ISetting setting = null;
            if (isSelect)
            {
                var settingExtension = settingType.Name;
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Title = $"请选择{settingExtension}",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Filter = $"配置文件|*.{settingExtension}"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var path = dialog.FileName;
                    setting = GetSetting(settingType, path);

                    ExcelHelper.GetEntity(settingType).SetSetting(setting);
                }
                else
                {
                    return;
                }
            }
            else
            {
                setting = GetSetting(settingType);
            }


            if (setting == null)
            {
                if (isSelect)
                {
                    //throw new IOException("打开的文件有误");
                    return;
                }
                else
                {
                    setting = Activator.CreateInstance(settingType) as ISetting;
                    UIEntityHelper.GetUIEntity(settingType).setting = (BaseSetting)setting;
                    SaveSetting(setting);
                }
            }
            else
            {
                if (isSelect)
                {
                    SaveSetting(setting);
                }
            }
            
            ExcelHelper.GetEntity(settingType).SetSetting(setting);
        }
    }
}
