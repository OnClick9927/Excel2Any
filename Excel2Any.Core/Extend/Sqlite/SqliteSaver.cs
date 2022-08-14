using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Excel2Any
{
    [Entity(typeof(SqliteEntity))]
    class SqliteSaver : DbSaver
    {
        public SqliteSaver(ISetting setting) : base(setting)
        {
            extension = "db";
        }
    }
}
