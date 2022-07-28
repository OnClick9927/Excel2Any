namespace Excel2Other
{
    [EntityCode((int)ConvertType.Xml)]
    public class XmlEntity : Entity
    {
        public XmlEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
