using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Excel2Any
{
    [Entity(typeof(CSVEntity))]
    public class CSVConverter : IConverter
    {
        private CSVSetting _setting;
        public CSVConverter(ISetting setting)
        {
            _setting = (CSVSetting)setting;
        }
        public List<SheetData> Convert(RawDataDetail rawData)
        {
            var sheetData = new List<SheetData>();
            for (int sheetIndex = 0; sheetIndex < rawData.data.Tables.Count; sheetIndex++)
            {
                var sheet = rawData.data.Tables[sheetIndex];
                var head = rawData.headsCollection[sheetIndex];
                StringBuilder sb = new StringBuilder();

                //判断是否排除标题
                var sheetName = sheet.TableName;
                if (!_setting.HideTitle)
                {
                    for (int i = 0; i < head.Count; i++)
                    {
                        var value = head[i].fieldName;
                        value = value.Replace("\"", _setting.quotes).Replace(",", _setting.dot) + ",";
                        sb.Append(value);
                    }
                    sb.Append(Environment.NewLine);
                }

                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    for (int j = 0; j < sheet.Columns.Count; j++)
                    {
                        var value = sheet.Rows[i][j].ToString();
                        if (value.StartsWith("[") && !value.EndsWith("]"))
                        {
                            if (value.EndsWith(","))
                            {
                                value = value.Substring(0, value.Length - 1);
                            }
                            value += "]";
                        };
                        value = value.Replace("\"", _setting.quotes).Replace(",", _setting.dot) + ",";
                        sb.Append(value);
                    }
                    sb.Append(Environment.NewLine);
                }


                sheetData.Add(new SheetData(sheetName, new TextContent(sb.ToString())));
            }

            return sheetData;
        }

        public ISetting GetSetting()
        {
            return _setting;
        }

        public void SetSetting(ISetting setting)
        {
            this._setting = (CSVSetting)setting;
        }
    }
}
