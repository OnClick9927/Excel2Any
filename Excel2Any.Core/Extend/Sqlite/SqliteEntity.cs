namespace Excel2Any
{
    [EntityCode((int)ConvertType.Sqlite)]
    public class SqliteEntity : Entity
    {
        public SqliteEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
