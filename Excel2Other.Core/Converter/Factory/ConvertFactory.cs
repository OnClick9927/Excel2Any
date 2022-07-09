using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{
    class ConvertFactory
    {
        public static IConverter CreateConverter(ConvertType type,ISetting settings)
        {
            switch (type)
            {
                case ConvertType.CSharp:
                    return new CSharpConverter(settings);
                case ConvertType.Json:
                    return new JsonConverter(settings);
                case ConvertType.Xml:
                    return new XmlConverter(settings);
                case ConvertType.Sqlite:
                    return new SqliteConverter(settings);
                default:
                    return null;
            }
        }
    }
}
