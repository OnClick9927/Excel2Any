namespace Excel2Other.Winform
{
    public class FormSetting : ISetting
    {
        /// <summary>
        /// 上次打开路径
        /// </summary>
        public string lastOpenPath;
        /// <summary>
        /// 是否打开上次路径
        /// </summary>
        public bool openLast;
        /// <summary>
        /// 排除前缀
        /// </summary>
        public string excludePrefix;
        /// <summary>
        /// 是否排除文件（读取文件的时候使用）
        /// </summary>
        public bool excludeFile;

        /// <summary>
        /// 是否扩展左边的栏（调整宽度）
        /// </summary>
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
