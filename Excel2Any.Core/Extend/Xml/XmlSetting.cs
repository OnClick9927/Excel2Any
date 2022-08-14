
namespace Excel2Any
{
    [Entity(typeof(XmlEntity))]
    public class XmlSetting :BaseSetting
    {
        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public string dateFormat;

        /// <summary>
        /// 是否显示字段为空的项
        /// </summary>
        public bool saveSpace;

        /// <summary>
        /// 描述行号设置
        /// </summary>
        //[SettingAttribute("字段描述行号", "Excel表中对应字段描述的行号（从1开始）")] 
        //public int CommentRowNum;

        /// <summary>
        /// 字段名行号设置
        /// </summary>
        [SettingAttribute("字段名行号", "Excel表中对应字段名的行号（从1开始）")] 
        public int FieldRowNum;

        /// <summary>
        /// 字段类型行号设置
        /// </summary>
        //[SettingAttribute("字段类型行号", "Excel表中对应字段类型的行号（从1开始）")] 
        //public int TypeRowNum;

        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        [SettingAttribute("内容开始行号", "Excel表中对应内容开始的行号（从1开始）")]
        public int StartRowNum;

        /// <summary>
        /// 是否将Sheet拆开保存
        /// </summary>
        [SettingAttribute("根据Sheet拆分文件", "将多个Sheet的Excel文件以Sheet名解析为多个文件")]
        public bool separateBySheet;
        public XmlSetting() : base()
        {
            dateFormat = "yyyy/MM/dd";      //日期保存格式
            saveSpace = false;              //是否将单元格为空的字段读取出来（如果为是则需要写入默认值）
            separateBySheet = true;
            FieldRowNum = 1;                //字段名所在行号
            StartRowNum = 4;                //内容开始的行号
        }
    }
}
