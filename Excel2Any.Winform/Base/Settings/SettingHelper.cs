using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public class SettingHelper
    {
        /// <summary>
        /// 保存主窗体
        /// </summary>
        public static Form form;
        #region 全局设置
        /// <summary>
        /// 全局设置
        /// </summary>
        public static FormSetting formSetting;

        static SettingHelper()
        {
            formSetting = (FormSetting)GetFormSetting();
        }


        private static ISetting GetFormSetting()
        {
            Type settingType = typeof(FormSetting);
            var ISetting = GetSettingBySettingType(settingType, GetDefaultSettingPath(settingType));
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

        public static void SaveFormSetting()
        {
            if (formSetting != null)
            {
                SaveSetting(formSetting, GetDefaultSettingPath(typeof(FormSetting)));
            }
        }

        #endregion

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
        private static string GetDefaultSettingPath(Type settingType, string plan = "")
        {
            if (settingType == typeof(FormSetting))
            {
                return $"Settings/config.{settingType.Name}";
            }
            else
            {
                if (string.IsNullOrEmpty(plan))
                {
                    plan = formSetting.plan;
                }
                //return Path.Combine(rootPath, $"Settings/config.{type.Name}");
                return $"Settings/{plan}/config.{settingType.Name}";
            }

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
                var typeSetting = set.GetType();
                path = GetDefaultSettingPath(typeSetting);
            }
            //文件夹如果不存在则创建
            FileInfo fi = new FileInfo(path);
            if (!Directory.Exists(fi.DirectoryName))
            {
                Directory.CreateDirectory(fi.DirectoryName);
            }
            File.WriteAllText(path, str);
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
        public static void LoadSetting(Type entityType, bool isSelect = false)
        {
            ISetting setting;

            Type settingType = UIEntityHelper.GetUIEntity(entityType).setting.GetType();
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
                    setting = GetSetting(entityType, path);
                    UIEntityHelper.GetUIEntity(entityType).setting = (BaseSetting)setting;
                    ExcelHelper.GetEntity(entityType).SetSetting(setting);
                }
                else
                {
                    return;
                }
            }
            else
            {
                setting = GetSetting(entityType);
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
                    UIEntityHelper.GetUIEntity(entityType).setting = (BaseSetting)setting;
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

            ExcelHelper.GetEntity(entityType).SetSetting(setting);
        }

        public static List<string> GetPlanList()
        {
            List<string> plans = new List<string>();
            var path = "Settings";

            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dii = dir.GetDirectories();

            foreach (DirectoryInfo info in dii)
            {
                plans.Add(info.Name);
            }

            return plans;
        }

        public static bool CreatePlan(string planName)
        {
            var path = $"Settings/{planName}";
            if (Directory.Exists(path))
            {
                return false;
            }
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DeletePlan(string planName)
        {
            var path = $"Settings/{planName}";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public static void ReNamePlan(string oldName, string newName)
        {
            var path1 = $"Settings/{oldName}";
            var path2 = $"Settings/{newName}";
            if (string.IsNullOrWhiteSpace(oldName))
            {
                CreatePlan(newName);
            }
            else if (Directory.Exists(path1))
            {
                Directory.Move(path1, path2);
            }
        }

    }
}
