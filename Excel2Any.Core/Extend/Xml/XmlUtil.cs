using System.Xml;

namespace Excel2Any
{
    public class XmlUtil
    {
        public static void AddXmlHeader(XmlDocument xml)
        {
            if (xml == null) return;
            XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(declaration);
        }

    }

    public static class XmlEx
    {
        public static void AddXmlHeader(this XmlDocument xml)
        {
            if (xml == null) return;
            XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(declaration);
        }
    }

}
