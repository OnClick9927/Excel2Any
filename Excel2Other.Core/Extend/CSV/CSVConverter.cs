using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Excel2Other
{
    [Entity(typeof(CSVEntity))]
    public class CSVConverter : IConverter
    {
        private CSVSetting _setting;
        public CSVConverter(ISetting setting)
        {
            _setting = (CSVSetting)setting;
        }
        public List<SheetData> Convert(DataSet data)
        {
            var sheetData = new List<SheetData>();
            foreach (DataTable sheet in data.Tables)
            {
                //排除sheet包含头
                //排除列数为0的sheet
                //排除第一列时判断列数
                //没有内容的跳过
                if ((_setting.excludeSheet && !string.IsNullOrWhiteSpace(_setting.excludePrefix) && sheet.TableName.StartsWith(_setting.excludePrefix))
                    || sheet.Columns.Count < 0
                    || (_setting.excludeFirstCol && sheet.Columns.Count < 1)
                    || (_setting.StartRowNum > sheet.Rows.Count - 1)
                    )
                {
                    continue;
                }

                #region 内容
                StringBuilder sb = new StringBuilder();

                //判断是否排除第一列
                var startCol = _setting.excludeFirstCol ? 1 : 0;
                var sheetName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;
                var endRowNum = (_setting.EndRowNum < 0 || _setting.EndRowNum > sheet.Rows.Count) ? sheet.Rows.Count - 1 : _setting.EndRowNum;
                //标题设置
                if (_setting.FieldRowNum >= 0 && _setting.FieldRowNum <= sheet.Rows.Count - 1)
                {
                    for (int i = startCol; i < sheet.Columns.Count; i++)
                    {
                        var value = sheet.Rows[_setting.FieldRowNum][i].ToString();
                        value = value.Replace("\"", _setting.quotes).Replace(",", _setting.dot) + ",";
                        sb.Append(value);
                    }
                    sb.Append(Environment.NewLine);
                }

                for (int i = _setting.StartRowNum; i < endRowNum; i++)
                {
                    for (int j = startCol; j < sheet.Columns.Count; j++)
                    {
                        var value = sheet.Rows[i][j].ToString();
                        value = value.Replace("\"", _setting.quotes).Replace(",", _setting.dot) + ",";
                        sb.Append(value);
                    }
                    sb.Append(Environment.NewLine);
                }

                #endregion

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
