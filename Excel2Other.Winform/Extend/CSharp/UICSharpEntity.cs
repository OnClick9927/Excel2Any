namespace Excel2Other.Winform
{
    [Entity(typeof(CSharpEntity))]
    public class UICSharpEntity : UIEntity
    {
        public override string name { get; } = "C#";
        public override BaseConvertPage page { get; } = new CSharpConvertPage();

        public override BaseSetting setting { get; set; } = new CSharpSetting();

        public override int symbol { get; } = 261897;

        public override int pageIndex { get; } = 1003;
    }
}
