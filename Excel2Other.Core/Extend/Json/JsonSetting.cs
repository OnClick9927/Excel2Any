namespace Excel2Other
{
    [Entity(typeof(JsonEntity))]
    public class JsonSetting : BaseSetting
    {

        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        [SettingAttribute(
            "日期格式",
            "将日期解析成指定格式的字符串",
            priority = 100
            )]
        public string dateFormat;
        /// <summary>
        /// 是否转化Json对象或数组内容的单元格
        /// </summary>
        [SettingAttribute(
            "单元格内的Json解析",
            "将单元格内的Json字符串进一步解析出来",
            priority = 101
            )]
        public bool JsonCell;

        /// <summary>
        /// 是否将所有的值用字符串表示
        /// </summary>
        [SettingAttribute(
            "内容解析为字符串",
            "将所有内容都解析成字符串形式",
            priority = 102
            )]
        public bool allString;


        /// <summary>
        /// 是否显示字段为空的项
        /// </summary>
        [SettingAttribute(
            "空单元格保留",
            "遇到空单元格时，保留字段并赋予默认值",
            priority = 103
            )]
        public bool saveSpace;

        /// <summary>
        /// 是否将Sheet拆开保存
        /// </summary
        [SettingAttribute(
            "根据Sheet拆分文件",
            "将多个Sheet的Excel文件以Sheet名解析成多个文件",
            priority = 104
            )]
        public bool separateBySheet;

        /// <summary>
        /// 字段名行号设置
        /// </summary>
        [SettingAttribute(
            "字段名行号",
            "Excel表中对应字段名的行号（从1开始）",
            priority = 105
            )]
        public int FieldRowNum;
        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        [SettingAttribute(
            "内容开始行号",
            "Excel表中对应内容开始的行号（从1开始）",
            priority = 106
            )]
        public int StartRowNum;

        public JsonSetting() : base()
        {
            dateFormat = "yyyy/MM/dd";      //日期保存格式
            JsonCell = true;                //是否将单元格里的Json解析出来
            allString = false;              //是否将所有值以字符串读取
            saveSpace = false;              //是否将单元格为空的字段读取出来（如果为是则需要写入默认值）
            separateBySheet = true;
            FieldRowNum = 1;                //字段名所在行号
            StartRowNum = 4;                //内容开始的行号
        }
    }
}
