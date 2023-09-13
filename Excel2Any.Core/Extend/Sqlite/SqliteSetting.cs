using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    [Entity(typeof(SqliteEntity))]
    public class SqliteSetting : BaseSetting
    {


        public SqliteSetting() : base()
        {

        }
    }
}
