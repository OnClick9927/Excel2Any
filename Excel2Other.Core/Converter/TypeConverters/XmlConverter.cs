using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace Excel2Other
{
    public class XmlConverter : IConverter
    {
        public string Extension => "xml";
        private string _fileName;

        char[] splitChar = new char[] { '\r', '\n' };
        private XmlSettings _setting;
        private List<SheetContent> _sheets = new List<SheetContent>();
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
                        sb.AppendLine($"<{_fileName}>");
                        for (int i = 0; i < _sheets.Count; i++)
                        {
                            //给内容加上缩进，暂时没找到好一点的方法
                            var content = _sheets[i].content.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < content.Length; j++)
                            {
                                sb.AppendLine($"  {content[j]}");
                            }
                        }
                        sb.AppendLine($"</{_fileName}>");
                        return new List<SheetContent>() { new SheetContent(_fileName, sb.ToString()) };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        public XmlConverter(XmlSettings setting)
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
                if ((_setting.excludeSheet && !string.IsNullOrWhiteSpace(_setting.excludePrefix) && sheet.TableName.StartsWith(_setting.excludePrefix))
                    || sheet.Columns.Count < 0
                    || (_setting.excludeFirstCol && sheet.Columns.Count < 1))
                {
                    continue;
                }
                //如果 字段名 开始行号有误则跳过
                else if ((_setting.FieldRowNum > sheet.Rows.Count - 1)
                    || (_setting.StartRowNum > sheet.Rows.Count - 1))
                {
                    continue;
                }
                else
                {
                    ConvertSheet(sheet);
                }
            }
        }

        /// <summary>
        /// 用于保存表头和索引
        /// </summary>
        struct RowHead
        {
            public string fieldName;
            public int index;

            public RowHead(string rowName, int index)
            {
                this.fieldName = rowName;
                this.index = index;
            }
        }

        private void ConvertSheet(DataTable sheet)
        {
            //xmldoc
            var doc = new XmlDocument();
            //XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            //doc.AppendChild(declaration);

            //保存表头的索引
            List<RowHead> rowHeads = new List<RowHead>();

            //保存每行的数据
            List<object> rows = new List<object>();

            int startCol; //开始列号
            string sheetName; //Sheet名

            //判断是否排除第一列
            startCol = _setting.excludeFirstCol ? 1 : 0;
            sheetName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;

            //列表头索引和名字获取
            for (int i = startCol; i < sheet.Columns.Count; i++)
            {
                var fieldName = sheet.Rows[_setting.FieldRowNum][i].ToString();
                if (string.IsNullOrWhiteSpace(fieldName) || fieldName.Contains("#")) continue; //xml不能包含#
                rowHeads.Add(new RowHead(fieldName, i));
            }
            if (rowHeads.Count == 0) return;

            XmlElement root = doc.CreateElement($"{sheetName}s");

            //遍历每行根据表头转换成对象字典
            for (int i = _setting.StartRowNum; i < sheet.Rows.Count; i++)
            {
                //每一行 读取一个对象，以sheet名包围
                //创建一个子节点
                XmlElement child = doc.CreateElement(sheetName);

                //遍历表头获取值并作为子节点的子节点
                for (int j = 0; j < rowHeads.Count; j++)
                {
                    //获取值
                    int columnIndex = rowHeads[j].index;
                    object value = sheet.Rows[i][columnIndex];


                    //为空时的处理
                    if (value.GetType() == typeof(System.DBNull) && string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        continue;
                    }
                    // 去掉数值字段的“.0”
                    if (value.GetType() == typeof(double))
                    {
                        double num = (double)value;
                        if ((int)num == num)
                            value = (int)num;
                    }

                    XmlElement innerChild = doc.CreateElement(rowHeads[j].fieldName);
                    innerChild.InnerText = value.ToString();
                    child.AppendChild(innerChild);
                }
                if (child.ChildNodes.Count != 0)
                {
                    root.AppendChild(child);
                }
            }
            doc.AppendChild(root);
            _sheets.Add(new SheetContent(sheetName, formatXml(doc)));
        }

        private string formatXml(XmlDocument xml)
        {
            XmlDocument xd = xml as XmlDocument;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw == null)
                    xtw.Close();
            }
            return sb.ToString();
        }

        public void SetSetting(ISettings setting)
        {
            _setting = (XmlSettings)setting;
        }

        public ISettings GetSetting()
        {
            return _setting;
        }
    }
}
