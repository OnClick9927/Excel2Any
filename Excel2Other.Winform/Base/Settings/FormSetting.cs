namespace Excel2Other.Winform
{
    public class FormSetting : ISetting
    {
        /// <summary>
        /// 上次打开路径（不显示出来）
        /// </summary>
        public string lastOpenPath;
        /// <summary>
        /// 是否打开上次路径
        /// </summary>
        [SettingAttribute("打开上一次的文件夹", "在程序打开时自动打开上一次打开的文件夹")]
        public bool openLast;
        /// <summary>
        /// 排除前缀
        /// </summary>
        [SettingAttribute("排除前缀", "在读取时排除以特定字符开头的文件名")]
        public string excludePrefix;
        /// <summary>
        /// 是否排除文件（读取文件的时候使用）
        /// </summary>
        [SettingAttribute("根据前缀排除文件", "读取文件夹时，跳过带排除前缀的文件名和文件夹")] 
        public bool excludeFile;

        /// <summary>
        /// 是否扩展左边的栏（调整宽度）
        /// </summary>
        [SettingAttribute("侧边栏显示名称", "侧边导航栏显示名称，并关闭提示")] 
        public bool isExpand;

        public FormSetting()
        {
            lastOpenPath = "";
            openLast = false;
            excludePrefix = "#";
            excludeFile = true;
            isExpand = false;
        }
    }
}
