using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public class SettingHelper
    {
        public static FormSetting formSetting;

        static SettingHelper()
        {
            formSetting = (FormSetting)GetSettingBySettingType(typeof(FormSetting));
        }

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
            if (settingType == typeof(FormSetting) || settingType ==typeof(CommonSetting))
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
        public static ISetting GetSettingBySettingType(Type settingType, string path = "")
        {
            if (!File.Exists(path))
            {
                return Activator.CreateInstance(settingType) as ISetting;
            }
            var str = File.ReadAllText(path);
            var setting = JsonConvert.DeserializeObject(str, settingType) as ISetting;

            return setting;
        }

        public static ISetting GetSettingBySettingType(Type settingType)
        {
            var path = GetDefaultSettingPath(settingType);
            return GetSettingBySettingType(settingType, path);
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

        /// <summary>
        /// 保存设置文件
        /// </summary>
        /// <param name="entityType">保存类型</param>
        /// <param name="isSelect">是否需要选择保存的路径</param>
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
            ExcelHelper.SetAllResultsDirty(entityType);
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
            DirectoryInfo[] dii;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                _=CreatePlan("default");
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            dii = dir.GetDirectories();

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
        public static void RenamePlan(string oldName, string newName)
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
        public static void SetPlan(string planName="")
        {
            if (string.IsNullOrEmpty(planName))
            {
                planName = "default";
            }
            var setting = formSetting;
            if (!setting.plan.Equals(planName))
            {
                setting.plan = planName;
                SaveSetting(setting);
            }
            UIEntityHelper.ReadSetting();
        }
    }
}
