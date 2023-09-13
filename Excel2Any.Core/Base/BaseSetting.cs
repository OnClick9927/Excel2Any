namespace Excel2Any
{
    public class BaseSetting : ISetting
    {
        /// <summary>
        /// 保存时的路径
        /// </summary>
        [SettingAttribute(
            "保存路径",
            "保存时的默认路径，为空时默认在桌面",
            textType = StringType.Directory,
            priority = 1
            )]
        public string savePath;

        /// <summary>
        /// Ctor
        /// </summary>
        public BaseSetting()
        {
            savePath = ""; //保存路径
        }

    }
}
