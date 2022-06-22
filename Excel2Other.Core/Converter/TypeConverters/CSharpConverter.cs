using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Excel2Other
{
    public class CSharpConverter : IConverter
    {
        public string Extension => "cs";
        private List<SheetContent> _sheets = new List<SheetContent>();

        private string _fileName;
        public List<SheetContent> Sheets
        {
            get
            {
                if (_setting.separateBySheet)
                {
                    return _sheets;
                }
                else
                {
                    if (_sheets != null && _sheets.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < _sheets.Count; i++)
                        {
                            sb.AppendLine(_sheets[i].content);
                            sb.AppendLine();
                        }
                        return new List<SheetContent>() { new SheetContent(_fileName, sb.ToString()) };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        private CSharpSettings _setting;
        public CSharpConverter(CSharpSettings setting)
        {
            _setting = setting;
        }

        public void Convert(DataSet data)
        {
            _fileName = data.DataSetName;
            _sheets.Clear();
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

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"public class {sheet.TableName}\r\n{{");

                #region 内容
                int startCol;//开始列数
                string sheetName;//Sheet名
                //判断是否排除第一列
                startCol = _setting.excludeFirstCol ? 1 : 0;
                sheetName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;

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

                    string fieldComment = "";
                    if (_setting.CommentRowNum <= sheet.Rows.Count - 1)
                    {
                        fieldComment = sheet.Rows[_setting.CommentRowNum][i].ToString();
                        if (!string.IsNullOrWhiteSpace(fieldComment))
                        {
                            fieldComment = "//" + fieldComment;
                            fieldComment.Replace("\n", "_"); //替换掉回车 不然单元格带回车的奇奇怪怪
                        }
                    }


                    sb.AppendLine($"\tpublic {fieldType} {fieldName}; {fieldComment}");
                }
                #endregion

                sb.Append('}');

                _sheets.Add(new SheetContent(sheetName, sb.ToString()));
            }
        }

        public void SetSetting(ISettings setting)
        {
            _setting = (CSharpSettings)setting;
        }

        public ISettings GetSetting()
        {
            return _setting;
        }
    }
}
