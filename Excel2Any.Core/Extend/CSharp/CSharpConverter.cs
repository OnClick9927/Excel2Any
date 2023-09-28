using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Excel2Any
{
    [Entity(typeof(CSharpEntity))]
    public class CSharpConverter : IConverter
    {
        private CSharpSetting _setting;
        public CSharpConverter(ISetting setting)
        {
            _setting = (CSharpSetting)setting;
        }

        public List<SheetData> Convert(RawDataDetail rawData)
        {
            
            var sheetData = new List<SheetData>();
            StringBuilder totalContent = new StringBuilder();

            for (int i = 0; i < rawData.headsCollection.Count; i++)
            {
                var heads = rawData.headsCollection[i];
                var sheet = rawData.data.Tables[i];

                #region 内容
                StringBuilder sb = new StringBuilder();

                var sheetName = sheet.TableName;
                sb.AppendLine($"public {(_setting.IsPartial? "partial ":"")}class {sheetName}\r\n{{");
                //遍历列 根据配置里设置的行号来确认字段的类型、名称和描述
                for (int j = 0; j < heads.Count; j++)
                {
                    var fieldName = heads[j].fieldName;
                    if (string.IsNullOrWhiteSpace(fieldName)) continue;

                    var fieldType = heads[j].typeName;

                    var summary = new StringBuilder();
                    var fieldComment = heads[j].comment;
                    if (!string.IsNullOrWhiteSpace(fieldComment))
                    {
                        summary.AppendLine("\t/// <summary>");
                        foreach (var tempString in fieldComment.Replace("\r", "").Split('\n'))
                        {
                            summary.AppendLine($"\t/// {tempString}");
                        }
                        summary.AppendLine("\t/// </summary>");
                    }
                    sb.Append(summary);
                    sb.AppendLine($"\tpublic {fieldType} {fieldName}{(_setting.IsProperty ? "{ get; set; }" : ";")}\n");
                }

                sb.Append('}');
                #endregion

                //判断是否要将sheet区分开
                if (_setting.separateBySheet && sb.Length > 0)
                {
                    sheetData.Add(new SheetData(sheetName, new TextContent(sb.ToString())));
                }
                else
                {
                    totalContent.Append(sb);
                    totalContent.AppendLine();
                }
            }

            if (!_setting.separateBySheet && totalContent.Length > 0)
            {
                sheetData.Add(new SheetData(rawData.data.DataSetName, new TextContent(totalContent.ToString())));
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
