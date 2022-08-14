using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace Excel2Any
{
    [Entity(typeof(XmlEntity))]
    public class XmlConverter : IConverter
    {
        private XmlSetting _setting;

        public XmlConverter(ISetting setting)
        {
            _setting = (XmlSetting)setting;
        }
        public List<SheetData> Convert(DataSet data)
        {
            List<SheetData> allSheetData = new List<SheetData>();

            //xmldoc
            var allSheetDataXml = new XmlDocument();
            allSheetDataXml.AddXmlHeader();

            //XmlElement rootElement = allSheetDataXml.CreateElement(data.DataSetName);
            XmlElement rootElement = allSheetDataXml.CreateElement("root");
            allSheetDataXml.AppendChild(rootElement);

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
                    if (_setting.separateBySheet)
                    {
                        if (allSheetDataXml != null)
                        {
                            var sheetData = new XmlDocument();
                            var convertedData = ConvertSheet(sheet, sheetData, out string sheetName);
                            sheetData.AddXmlHeader();
                            sheetData.AppendChild(convertedData);
                            allSheetData.Add(new SheetData(sheetName, new TextContent(formatXml(sheetData))));
                        }
                    }
                    else
                    {
                        var convertedData = ConvertSheet(sheet, allSheetDataXml, out string sheetName);
                        rootElement.AppendChild(convertedData);
                    }
                }
            }

            if (!_setting.separateBySheet)
            {
                allSheetData.Add(new SheetData(data.DataSetName, new TextContent(formatXml(allSheetDataXml))));
            }
            return allSheetData;
        }

        private XmlNode ConvertSheet(DataTable sheet,XmlDocument xd,out string sheetName)
        {

            //保存表头的索引
            List<RowHead> rowHeads = new List<RowHead>();

            int startCol; //开始列号

            //判断是否排除第一列
            startCol = _setting.excludeFirstCol ? 1 : 0;
            sheetName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;

            sheetName = sheetName.Replace("\n","");
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "Empty";
            }
            //列表头索引和名字获取
            for (int i = startCol; i < sheet.Columns.Count; i++)
            {
                var fieldName = sheet.Rows[_setting.FieldRowNum][i].ToString();
                fieldName = fieldName.Replace("\n", "").Replace("#","").Replace("-","").Replace(" ", ""); //xml不能包含的字符
                if (string.IsNullOrWhiteSpace(fieldName)) continue; 
                rowHeads.Add(new RowHead(fieldName, i));
            }
            if (rowHeads.Count == 0) return null;

            XmlElement root = xd.CreateElement($"{sheetName}s");

            //遍历每行根据表头转换成对象字典
            for (int i = _setting.StartRowNum; i < sheet.Rows.Count; i++)
            {
                //每一行 读取一个对象，以sheet名包围
                //创建一个子节点
                XmlElement child = xd.CreateElement(sheetName);

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
                    value = DataValueUtil.GetIntValue(value);

                    XmlElement innerChild = xd.CreateElement(rowHeads[j].fieldName);
                    innerChild.InnerText = value.ToString();
                    child.AppendChild(innerChild);
                }
                if (child.ChildNodes.Count != 0)
                {
                    root.AppendChild(child);
                }
            }
            return root;
        }

        private string formatXml(XmlDocument xd)
        {
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

        public void SetSetting(ISetting setting)
        {
            _setting = (XmlSetting)setting;
        }

        public ISetting GetSetting()
        {
            return _setting;
        }
    }
}
