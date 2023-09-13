namespace Excel2Any
{
    [Entity(typeof(CSVEntity))]
    public class CSVSetting : BaseSetting
    {
        /// <summary>
        /// 标题设置
        /// </summary>
        [SettingAttribute(
            "隐藏字段名",
            "隐藏掉字段名只显示内容(即不显示标题)",
            priority = 103
            )]
        public bool HideTitle;
        /// <summary>
        /// 替换逗号的字符
        /// </summary>
        [SettingAttribute(
            "替换逗号的字符",
            "对单元格内的逗号进行处理，将其替换为指定字符",
            priority = 104
            )]
        public string dot;
        /// <summary>
        /// 替换双引号的字符
        /// </summary>
        [SettingAttribute(
            "替换双引号的字符",
            "对单元格内的双引号进行处理，将其替换为指定字符",
            priority = 105
            )]
        public string quotes;
        public CSVSetting() : base()
        {
            HideTitle = false;
            dot = "◞";
            quotes = "◜";
        }
    }
}
