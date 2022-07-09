namespace Excel2Other
{
    public class CSharpSetting:BaseSetting
    {

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
        /// 是否为属性器
        /// </summary>
        public bool IsProperty;


        public CSharpSetting() : base()
        {
            CommentRowNum = 2;              //字段描述所在行号
            TypeRowNum = 1;                 //字段类型所在行号
            FieldRowNum = 0;                //字段名所在行号
            IsProperty = true;
        }
    }
}
