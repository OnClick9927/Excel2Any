namespace Excel2Other.Winform
{
    [Entity(typeof(CSVEntity))]
    public class UICSVEntity : UIEntity
    {
        public override string name { get; } = "CSV";
        public override BaseConvertPage page { get; } = new CSVConvertPage();

        public override BaseSetting setting { get; set; } = new CSVSetting();

        public override int symbol { get; } = 261897;

        public override int pageIndex { get; } = 1005;
    }
}
