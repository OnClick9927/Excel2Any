using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Excel2Other
{
    public class CSharpConverter : IConverter
    {
        private CSharpSetting _setting;
        public CSharpConverter(ISetting setting)
        {
            _setting = (CSharpSetting)setting;
        }

        public List<SheetData> Convert(DataSet data)
        {
            var sheetData = new List<SheetData>();
            StringBuilder totalContent = new StringBuilder();
            foreach (DataTable sheet in data.Tables)
            {
                //排除sheet包含头
                //排除列数为0的sheet
                //排除第一列时判断列数
                //没有字段名的跳过
                if ((_setting.excludeSheet && !string.IsNullOrWhiteSpace(_setting.excludePrefix) && sheet.TableName.StartsWith(_setting.excludePrefix))
                    || sheet.Columns.Count < 0
                    || (_setting.excludeFirstCol && sheet.Columns.Count < 1)
                    || (_setting.FieldRowNum > sheet.Rows.Count - 1))
                {
                    continue;
                }

                #region 内容
                int startCol;//开始列数
                string sheetName;//Sheet名
                //判断是否排除第一列
                startCol = _setting.excludeFirstCol ? 1 : 0;
                sheetName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;


                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"public class {sheetName}\r\n{{");
                //遍历列 根据配置里设置的行号来确认字段的类型、名称和描述
                for (int i = startCol; i < sheet.Columns.Count; i++)
                {
                    var fieldName = sheet.Rows[_setting.FieldRowNum][i].ToString();
                    if (string.IsNullOrWhiteSpace(fieldName)) continue;


                    //类型和描述放宽条件，越界就默认值
                    string fieldType = "";
                    if (_setting.TypeRowNum <= sheet.Rows.Count - 1)
                    {
                        fieldType = sheet.Rows[_setting.TypeRowNum][i].ToString();
                    }
                    //类型为空时设为object
                    if (string.IsNullOrWhiteSpace(fieldType))
                    {
                        fieldType = "object";
                    }

                    var summary = new StringBuilder();
                    if (_setting.CommentRowNum <= sheet.Rows.Count - 1)
                    {
                         var fieldComment = sheet.Rows[_setting.CommentRowNum][i].ToString();
                        
                        if (!string.IsNullOrWhiteSpace(fieldComment))
                        {
                            summary.AppendLine("\t/// <summary>");
                            foreach (var tempString in fieldComment.Replace("\r","").Split('\n'))
                            {
                                summary.AppendLine($"\t/// {tempString}");
                            }
                            summary.AppendLine("\t/// </summary>");
                        }
                    }
                    sb.Append(summary);
                    sb.AppendLine($"\tpublic {fieldType} {fieldName}{(_setting.IsProperty?"{ get; set; }":";")}\n");
                }

                sb.Append('}');
                #endregion

                //判断是否要将sheet区分开
                if (!_setting.separateBySheet && sb.Length > 0)
                {
                    sheetData.Add(new SheetData(sheetName,new TextContent(sb.ToString())));
                }
                else
                {
                    totalContent.Append(sb);
                    totalContent.AppendLine();
                }
            }

            if (!_setting.separateBySheet && totalContent.Length >0 )
            {
                sheetData.Add(new SheetData(data.DataSetName, new TextContent(totalContent.ToString())));
            }

            return sheetData;
        }

        public void SetSetting(ISetting setting)
        {
            _setting = (CSharpSetting)setting;
        }

        public ISetting GetSetting()
        {
            return _setting;
        }
    }
}
