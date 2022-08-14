

namespace Excel2Any 
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
