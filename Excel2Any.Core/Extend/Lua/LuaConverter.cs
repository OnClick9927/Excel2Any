using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    [Entity(typeof(LuaEntity))]
    public class LuaConverter : IConverter
    {

        private LuaSetting _setting;

        /// <summary>
        /// Ctor
        /// </summary>
        public LuaConverter(ISetting setting)
        {
            _setting = (LuaSetting)setting;
        }

        public List<SheetData> Convert(DataSet data)
        {
            List<SheetData> allSheetData = new List<SheetData>();


            foreach (DataTable sheet in data.Tables)
            {
                //排除sheet包含头
                //排除列数为0的sheet
                //排除第一列时判断列数
                if ((_setting.excludeSheet && !string.IsNullOrWhiteSpace(_setting.excludePrefix) && sheet.TableName.StartsWith(_setting.excludePrefix))
                    || sheet.Columns.Count < 0
                    || (_setting.excludeFirstCol && sheet.Columns.Count < 1))
                {
                    continue;
                }
                //如果 字段名 开始行号 字段类型 有误则跳过
                else if ((_setting.FieldRowNum > sheet.Rows.Count - 1)
                    || (_setting.StartRowNum > sheet.Rows.Count - 1)
                    || (_setting.TypeRowNum > sheet.Rows.Count - 1))
                {
                    continue;
                }
                else
                {
                    var sheetData = ConvertSheet(sheet, out string tableName);
                    if (sheetData != null)
                    {
                        allSheetData.Add(new SheetData(tableName, new TextContent(sheetData)));
                    }
                }
            }

            return allSheetData;
        }

        struct RowHeadPlus
        {
            public string fieldName;
            public string type;
            public int index;
        }
        public string ConvertSheet(DataTable sheet, out string tableName)
        {
            StringBuilder sb = new StringBuilder();

            //保存表头的索引
            List<RowHeadPlus> rowHeads = new List<RowHeadPlus>();
            //判断是否排除第一列
            int startCol = _setting.excludeFirstCol ? 1 : 0;
            tableName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;

            sb.AppendLine($"local {tableName} =");
            sb.AppendLine("{");




            //列表头索引和名字获取
            for (int i = startCol; i < sheet.Columns.Count; i++)
            {
                var fieldName = sheet.Rows[_setting.FieldRowNum][i].ToString();
                var fieldTypeName = sheet.Rows[_setting.TypeRowNum][i].ToString().ToLower().Trim();


                if (string.IsNullOrWhiteSpace(fieldName)) continue;
                var head = new RowHeadPlus
                {
                    type = fieldTypeName,
                    index = i,
                    fieldName = fieldName
                };

                rowHeads.Add(head);
            }

            for (int i = _setting.StartRowNum; i < sheet.Rows.Count; i++)
            {
                sb.Append("    {");
                for (int j = 0; j < rowHeads.Count; j++)
                {
                    object value = sheet.Rows[i][rowHeads[j].index];
                    #region 对单元格进行处理
                    //为空时的处理
                    if (value.GetType() != typeof(System.DBNull))
                    {
                        var str = value.ToString().ToLower().Trim();
                        switch (rowHeads[j].type)
                        {
                            case "bool":
                            case "boolean":
                                if (str.Equals("1") || str.Equals("true"))
                                {
                                    value = "true";
                                }
                                else
                                {
                                    value = "false";
                                }
                                break;
                            case "string":
                                value = $"\"{value}\"";
                                break;
                            default:
                                if (str.Contains("["))
                                {
                                    if (str.EndsWith(","))
                                    {
                                        str = str.Substring(0, str.Length - 1);
                                    }
                                    if (!str.EndsWith("]"))
                                    {
                                        str = str + "]";
                                    }
                                    value = str.Replace("[", "{").Replace("]", "}");
                                }

                                break;
                        }
                    }
                    else
                    {
                        value = "nil";
                    }

                    sb.Append($" {rowHeads[j].fieldName} = {value}{(j == rowHeads.Count - 1 ? "" : ",")}");
                    #endregion
                }
                sb.AppendLine("},");
            }



            sb.AppendLine("}");
            return sb.ToString();
        }


        public ISetting GetSetting()
        {
            return _setting;
        }

        public void SetSetting(ISetting setting)
        {
            _setting = (LuaSetting)setting;
        }
    }
}
