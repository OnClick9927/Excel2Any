namespace Excel2Other
{
    public class JsonSettings : BaseSettings
    {

        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public string dateFormat;
        /// <summary>
        /// 是否转化Json对象或数组内容的单元格
        /// </summary>
        public bool JsonCell;

        /// <summary>
        /// 是否将所有的值用字符串表示
        /// </summary>
        public bool allString;


        /// <summary>
        /// 是否显示字段为空的项
        /// </summary>
        public bool saveSpace;



        /// <summary>
        /// 字段名行号设置
        /// </summary>
        public int FieldRowNum;
        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        public int StartRowNum;

        public JsonSettings() : base()
        {
            dateFormat = "yyyy/MM/dd";      //日期保存格式
            JsonCell = true;                //是否将单元格里的Json解析出来
            allString = false;              //是否将所有值以字符串读取
            saveSpace = false;              //是否将单元格为空的字段读取出来（如果为是则需要写入默认值）

            FieldRowNum = 0;                //字段名所在行号
            StartRowNum = 3;                //内容开始的行号
        }
    }
}
