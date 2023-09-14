using System.Collections.Generic;
using System.Text;

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

        public List<SheetData> Convert(RawDataDetail rawData)
        {
            List<SheetData> allSheetData = new List<SheetData>();

            for (int i = 0; i < rawData.data.Tables.Count; i++)
            {
                var sheet = rawData.data.Tables[i];
                var rowHeads = rawData.headsCollection[i];
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"local {rawData.data.Tables[i].TableName} =");
                sb.AppendLine("{");

                for (int rowIndex = 0; rowIndex < sheet.Rows.Count; rowIndex++)
                {
                    sb.Append("    {");
                    for (int headIndex = 0; headIndex < rowHeads.Count; headIndex++)
                    {
                        object value = sheet.Rows[rowIndex][headIndex];
                        #region 对单元格进行处理
                        //为空时的处理
                        if (value.GetType() != typeof(System.DBNull))
                        {
                            var str = value.ToString().ToLower().Trim();
                            switch (rowHeads[headIndex].typeName)
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

                        sb.Append($" {rowHeads[headIndex].fieldName} = {value}{(headIndex == rowHeads.Count - 1 ? "" : ",")}");
                        #endregion
                    }
                    sb.AppendLine("},");
                }

                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendLine($"return {sheet.TableName}");
                allSheetData.Add(new SheetData(sheet.TableName, new TextContent(sb.ToString())));
            }

            return allSheetData;
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
