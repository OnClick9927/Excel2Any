namespace Excel2Any
{
    [Entity(typeof(CSharpEntity))]
    public class CSharpSetting : BaseSetting
    {
        /// <summary>
        /// 是否为partial
        /// </summary>
        [SettingAttribute(
            "使用partial类",
            "使用关键字partial定义生成的类",
            priority = 102
            )]
        public bool IsPartial;

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
            IsPartial = true;
            IsProperty = true;
            separateBySheet = true;         //是否根据Sheet保存成多个文件
        }
    }
}
