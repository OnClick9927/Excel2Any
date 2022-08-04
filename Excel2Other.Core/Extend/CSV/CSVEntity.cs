namespace Excel2Other
{
    [EntityCode((int)ConvertType.CSV)]
    public class CSVEntity : Entity
    {
        public CSVEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
