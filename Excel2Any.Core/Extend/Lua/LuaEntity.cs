using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    [EntityCode((int)ConvertType.Lua)]
    public class LuaEntity : Entity
    {
        public LuaEntity(IConverter c, IConverterSaver save) : base(c, save)
        {
        }
    }
}
