namespace Excel2Other.Winform
{
    [Entity(typeof(XmlEntity))]
    public class UIXmlEntity : UIEntity
    {
        public override string name { get; } = "Xml";
        public override BaseConvertPage page { get; } = new XmlConvertPage();

        public override BaseSetting setting { get; } = new XmlSetting();

        public override int symbol { get; } = 261891;

        public override int pageIndex { get; } = 1002;
    }
}
