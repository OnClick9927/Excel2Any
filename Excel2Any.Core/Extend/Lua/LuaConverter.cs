using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                sb.AppendLine($"local {rawData.data.Tables[i].TableName} ={{");

                var keyList = new List<int>();
                var notKeyList = new List<int>();

                var headList = rawData.headsCollection[i];
                for (int colIndex = 0; colIndex < headList.Count; colIndex++)
                {
                    if (headList[colIndex].isKey)
                    {
                        keyList.Add(colIndex);
                    }
                    else
                    {
                        notKeyList.Add(colIndex);
                    }
                }

                for (int rowIndex = 0; rowIndex < sheet.Rows.Count; rowIndex++)
                {
                    for (int j = 0; j < keyList.Count; j++)
                    {
                        int index = keyList[j];
                        object value = GetObjectValue(sheet.Rows[rowIndex][index], headList[index]);
                        sb.AppendLine($"{GetMulitiTab(j + 1)}[{value}] = {{");
                    }

                    for (int j = 0; j < notKeyList.Count; j++)
                    {
                        int index = notKeyList[j];
                        object value = GetObjectValue(sheet.Rows[rowIndex][index], headList[index]);
                        sb.AppendLine($"{GetMulitiTab(keyList.Count + 1)}{headList[index].fieldName} = {value}{(j != notKeyList.Count - 1 ? "," : "")}");
                    }

                    for (int j = 0; j < keyList.Count; j++)
                    {
                        sb.AppendLine($"{GetMulitiTab(keyList.Count - j)}}}{(rowIndex != sheet.Rows.Count - 1 && j == keyList.Count - 1 ? "," : "")}");
                    }
                }


                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendLine($"return {sheet.TableName}");
                allSheetData.Add(new SheetData(sheet.TableName, new TextContent(sb.ToString())));
            }

            return allSheetData;
        }

        private object GetObjectValue(object value, RowHead head)
        {
            if (value.GetType() != typeof(System.DBNull))
            {
                var str = value.ToString().ToLower().Trim();

                if (head.type.IsArray)
                {
                    StringBuilder sb = new StringBuilder("{");
                    if (head.type.GetArrayRank() == 1)
                    {
                        var arr = (Array)value;
                        for (int i = 0; i < arr.Length; i++)
                        {
                            var item = arr.GetValue(i);
                            if (item is string)
                            {
                                sb.Append($"\"{item}\"");
                            }
                            else
                            {
                                sb.Append($"{item}");
                            }

                            if (i != arr.Length -1)
                            {
                                sb.Append(',');
                            }
                        }
                    }
                    else if (head.type.GetArrayRank() == 2)
                    {
                        var arr = (Array)value;

                        int rows = arr.GetLength(0);
                        int columns = arr.GetLength(1);

                        // 遍历二维数组
                        for (int row = 0; row < rows; row++)
                        {
                            StringBuilder sb1 = new StringBuilder("{");
                            for (int col = 0; col < columns; col++)
                            {
                                var item = arr.GetValue(row, col);
                                if (item is string)
                                {
                                    sb1.Append($"\"{item}\"");
                                }
                                else
                                {
                                    sb1.Append($"{item}");
                                }

                                if (col != columns - 1)
                                {
                                    sb1.Append(',');
                                }
                            }
                            sb1.Append($"}}{(row!= rows -1 ? ",":"")}");

                            sb.Append(sb1);
                        }
                    }
                    sb.Append("}"); 
                    value = sb.ToString();
                }

                switch (head.typeName)
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
                }
            }
            else
            {
                value = "nil";
            }
            return value;
        }

        private StringBuilder GetMulitiTab(int count)
        {
            if (!tabDic.ContainsKey(count))
            {
                tabDic[count] = new StringBuilder(string.Concat(Enumerable.Repeat("\t", count)));
            }
            return tabDic[count];
        }
        private Dictionary<int, StringBuilder> tabDic = new Dictionary<int, StringBuilder>
        {
            { 1 , new StringBuilder("\t") },
            { 2 , new StringBuilder("\t\t") },
            { 3 , new StringBuilder("\t\t\t") },
            { 4 , new StringBuilder("\t\t\t\t") },
            { 5 , new StringBuilder("\t\t\t\t\t") },
        };


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
