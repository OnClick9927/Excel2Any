namespace Excel2Other
{
    [EntityCode((int)ConvertType.Json)]
    public class JsonEntity : Entity
    {
        public JsonEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
