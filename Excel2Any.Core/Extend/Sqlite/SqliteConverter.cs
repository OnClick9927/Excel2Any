using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    [Entity(typeof(SqliteEntity))]
    public class SqliteConverter : IConverter
    {
        private SqliteSetting _setting;
        public SqliteConverter(ISetting setting)
        {
            _setting = (SqliteSetting)setting;
        }
        public List<SheetData> Convert(RawDataDetail rawData)
        {
            var allSheetData = new List<SheetData>();
            foreach (DataTable item in rawData.data.Tables)
            {
                allSheetData.Add(new SheetData(item.TableName, new DataContent(item)));
            }
            return allSheetData;
        }

        public void SetSetting(ISetting setting)
        {
            _setting = (SqliteSetting)setting;
        }

        public ISetting GetSetting()
        {
            return _setting;
        }
    }
}
