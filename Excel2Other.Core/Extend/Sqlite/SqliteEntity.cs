namespace Excel2Other
{
    [EntityCode((int)ConvertType.Sqlite)]
    public class SqliteEntity : Entity
    {
        public SqliteEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
