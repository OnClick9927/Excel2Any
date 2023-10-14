using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
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
                        var value = sheet.Rows[i][j];
                        StringBuilder str = new StringBuilder();
                        var type = rawData.headsCollection[sheetIndex][j].type;
                        if (value.GetType() != typeof(System.DBNull))
                        {
                            if (type.IsArray)
                            {
                                str.Append("[");
                                if (type.GetArrayRank() == 1)
                                {
                                    var arr = (Array)value;
                                    for (int k = 0; k < arr.Length; k++)
                                    {
                                        var item = arr.GetValue(k);
                                        str.Append($"{item}");
                                        if (k != arr.Length - 1)
                                        {
                                            str.Append(_setting.dot);
                                        }
                                    }
                                }
                                else if (type.GetArrayRank() == 2)
                                {
                                    var arr = (Array)value;

                                    int rows = arr.GetLength(0);
                                    int columns = arr.GetLength(1);

                                    // 遍历二维数组
                                    for (int row = 0; row < rows; row++)
                                    {
                                        StringBuilder sb1 = new StringBuilder("[");
                                        for (int col = 0; col < columns; col++)
                                        {
                                            var item = arr.GetValue(row, col);
                                            sb1.Append($"{item}");

                                            if (col != columns - 1)
                                            {
                                                sb1.Append(_setting.dot);
                                            }
                                        }
                                        sb1.Append($"]{(row != rows - 1 ? _setting.dot : "")}");

                                        str.Append(sb1);
                                    }
                                }
                                str.Append("],");
                            }
                            else
                            {
                                sb.Append(value.ToString().Replace("\"", _setting.quotes).Replace(",", _setting.dot) + ",");
                            }
                        }
                        else
                        {
                            sb.Append(",");
                        }
                        sb.Append(str);
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
