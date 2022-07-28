namespace Excel2Other
{
    [EntityCode((int)ConvertType.CSharp)]
    public class CSharpEntity : Entity
    {
        public CSharpEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
