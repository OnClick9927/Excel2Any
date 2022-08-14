namespace Excel2Any
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true)]
    public class SettingAttribute : System.Attribute
    {
        public readonly string name;
        public readonly string des;
        public StringType textType;
        public int priority;
        public SettingAttribute(string name, string des)
        {
            this.name = name;
            this.des = des;
        }

    }

    public enum StringType
    {
        None = 0,
        Integer = 1,
        Double = 2,
        Directory = 3,
        Select = 4,
    }
}
