namespace Excel2Other
{
    [Entity(typeof(CSVEntity))]
    public class CSVSetting : BaseSetting
    {
        /// <summary>
        /// 开始行号
        /// </summary>
        [SettingAttribute(
            "开始行号",
            "内容开始的行号（从1开始）",
            priority = 100
            )]
        public int StartRowNum;
        /// <summary>
        /// 结束行号
        /// </summary>
        [SettingAttribute(
            "结束行号",
            "内容结束的行号，如果填0表示直到文件结束（从1开始）",
            priority = 101
            )]
        public int EndRowNum;
        /// <summary>
        /// 字段名行号设置
        /// </summary>
        [SettingAttribute(
            "字段名行号",
            "Excel表中对应字段名的行号（从1开始）",
            priority = 102
            )]
        public int FieldRowNum;
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
            StartRowNum = 4;                //内容开始的行号
            EndRowNum = -1;
            FieldRowNum = -1;               //字段名所在行号
            HideTitle = false;
            dot = "◞";
            quotes = "◜";
        }
    }
}
