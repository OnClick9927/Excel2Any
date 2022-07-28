

namespace Excel2Other 
{
    [Entity(typeof(XmlEntity))]
    public class XmlSaver : TextSaver
    {
        public XmlSaver(ISetting setting) : base(setting)
        {
            extension = "xml";
        }
    }
}
