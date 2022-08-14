namespace Excel2Any
{
    [Entity(typeof(CSharpEntity))]
    public class CSharpSetting : BaseSetting
    {

        /// <summary>
        /// 描述行号设置
        /// </summary>
        [SettingAttribute(
            "字段描述行号",
            "Excel表中对应字段描述的行号（从1开始）",
            priority = 100
            )]
        public int CommentRowNum;

        /// <summary>
        /// 字段名行号设置
        /// </summary>
        [SettingAttribute(
            "字段名行号",
            "Excel表中对应字段名的行号（从1开始）",
            priority = 101
            )]
        public int FieldRowNum;

        /// <summary>
        /// 字段类型行号设置
        /// </summary>
        [SettingAttribute(
            "字段类型行号",
            "Excel表中对应字段类型的行号（从1开始）",
            priority = 102
            )]
        public int TypeRowNum;
        /// <summary>
        /// 是否为属性器
        /// </summary>
        [SettingAttribute(
            "输出为属性器",
            "将字段输出为属性器（即{get;set;}）",
            priority = 103
            )]
        public bool IsProperty;

        /// <summary>
        /// 是否将Sheet拆开保存
        /// </summary>
        [SettingAttribute(
            "根据Sheet拆分文件",
            "将多个Sheet的Excel文件以Sheet名解析为多个文件",
            priority = 104
            )]
        public bool separateBySheet;

        public CSharpSetting() : base()
        {
            CommentRowNum = 0;              //字段描述所在行号
            TypeRowNum = 3;                 //字段类型所在行号
            FieldRowNum = 1;                //字段名所在行号
            IsProperty = true;
            separateBySheet = true;         //是否根据Sheet保存成多个文件
        }
    }
}
