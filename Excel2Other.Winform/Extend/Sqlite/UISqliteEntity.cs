namespace Excel2Other.Winform
{
    [Entity(typeof(SqliteEntity))]
    public class UISqliteEntity : UIEntity
    {
        public override string name { get; } = "Sqlite";
        public override BaseConvertPage page { get; } = new SqliteConvertPage();

        public override BaseSetting setting { get; set; } = new SqliteSetting();

        public override int symbol { get; } = 261787;

        public override int pageIndex { get; } = 1004;
    }
}
