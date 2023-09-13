
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
        /// 是否将Sheet拆开保存
        /// </summary>
        [SettingAttribute("根据Sheet拆分文件", "将多个Sheet的Excel文件以Sheet名解析为多个文件")]
        public bool separateBySheet;
        public XmlSetting() : base()
        {
            dateFormat = "yyyy/MM/dd";      //日期保存格式
            saveSpace = false;              //是否将单元格为空的字段读取出来（如果为是则需要写入默认值）
            separateBySheet = true;
        }
    }
}
