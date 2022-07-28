namespace Excel2Other
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true)]
    public class SettingAttribute : System.Attribute
    {
        public readonly string name;
        public readonly string des;
        public readonly int textType;
        public SettingAttribute(string name, string des)
        {
            this.name = name;
            this.des = des;
            textType = (int)StringType.None;
        }

        public SettingAttribute(string name, string des, int textType)
        {
            this.name = name;
            this.des = des;
            this.textType = textType;
        }
    }

    public enum StringType
    {
        None = 0,
        Integer = 1,
        Double = 2,
        Directorty = 3,
    }
}
