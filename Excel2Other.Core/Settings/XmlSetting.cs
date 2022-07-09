
namespace Excel2Other
{
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
        public int CommentRowNum;

        /// <summary>
        /// 字段名行号设置
        /// </summary>
        public int FieldRowNum;

        /// <summary>
        /// 字段类型行号设置
        /// </summary>
        public int TypeRowNum;

        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        public int StartRowNum;

        public XmlSetting() : base()
        {
            dateFormat = "yyyy/MM/dd";      //日期保存格式
            saveSpace = false;              //是否将单元格为空的字段读取出来（如果为是则需要写入默认值）

            CommentRowNum = 2;              //字段描述所在行号
            TypeRowNum = 1;                 //字段类型所在行号
            FieldRowNum = 0;                //字段名所在行号
            StartRowNum = 3;                //内容开始的行号
        }
    }
}
