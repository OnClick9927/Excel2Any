namespace Excel2Any
{
    [EntityCode((int)ConvertType.Xml)]
    public class XmlEntity : Entity
    {
        public XmlEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
