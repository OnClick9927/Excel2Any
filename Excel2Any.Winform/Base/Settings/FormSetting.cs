using System.IO;

namespace Excel2Any.Winform
{
    public class FormSetting : ISetting
    {
        [SettingAttribute("配置方案", "切换当前的设置方案", textType = StringType.Select, priority = 100)]
        /// <summary>
        /// 配置方案（默认为default）
        /// </summary>
        public string plan;
        /// <summary>
        /// 排除前缀
        /// </summary>
        [SettingAttribute("排除文件名字符", "在读取时排除以特定字符开头的文件名", priority = 101)]
        public string excludeFileString;
        /// <summary>
        /// 是否排除文件（读取文件的时候使用）
        /// </summary>
        [SettingAttribute("根据字符排除文件", "读取文件夹时，跳过带特定字符的文件名和文件夹", priority = 102)]
        public bool excludeFile;
        /// <summary>
        /// 是否打开上次路径
        /// </summary>
        [SettingAttribute("打开上一次的文件夹", "在程序打开时自动打开上一次打开的文件夹", priority = 103)]
        public bool openLast;
        /// <summary>
        /// 上次打开路径
        /// </summary>
        [SettingAttribute("打开上一次的文件夹路径", "上一次打开文件夹的路径", priority = 106)]
        public string lastOpenPath;

        /// <summary>
        /// 是否扩展左边的栏（调整宽度）
        /// </summary>
        [SettingAttribute("侧边栏显示名称", "侧边导航栏显示名称，并关闭提示", priority = 107)]
        public bool isExpand;

        public FormSetting()
        {
            openLast = false;
            lastOpenPath = "";
            isExpand = false;
            plan = "default";
            excludeFileString = "#";
            excludeFile = true;
        }
    }
}
