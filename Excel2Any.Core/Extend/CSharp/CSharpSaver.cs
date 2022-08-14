using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    [Entity(typeof(CSharpEntity))]
    public class CSharpSaver : TextSaver
    {
        public CSharpSaver(ISetting setting) : base(setting)
        {
            extension = "cs";
        }
    }
}
