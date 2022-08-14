namespace Excel2Any
{
    [EntityCode((int)ConvertType.Json)]
    public class JsonEntity : Entity
    {
        public JsonEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
