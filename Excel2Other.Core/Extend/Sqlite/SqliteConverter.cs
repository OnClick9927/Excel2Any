using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{
    [Entity(typeof(SqliteEntity))]
    public class SqliteConverter : IConverter
    {
        private SqliteSetting _setting;
        public SqliteConverter(ISetting setting)
        {
            _setting = (SqliteSetting)setting;
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
                    var sheetData = ConvertSheet(sheet);
                    if (sheetData != null)
                    {
                        allSheetData.Add(new SheetData(sheetData.TableName, new DataContent(sheetData)));
                    }
                }
            }

            return allSheetData;
        }
        public DataTable ConvertSheet(DataTable sheet)
        {
            //保存表头的索引
            List<RowHead> rowHeads = new List<RowHead>();
            DataTable data = new DataTable();
            //判断是否排除第一列
            int startCol = _setting.excludeFirstCol ? 1 : 0;
            data.TableName = _setting.excludeFirstCol ? sheet.Rows[0][0].ToString() : sheet.TableName;

            //列表头索引和名字获取
            for (int i = startCol; i < sheet.Columns.Count; i++)
            {
                var fieldName = sheet.Rows[_setting.FieldRowNum][i].ToString();
                var fieldTypeName = sheet.Rows[_setting.TypeRowNum][i].ToString().ToLower().Trim();

                var fieldType = FieldTypeUtil.GetType(fieldTypeName);


                if (string.IsNullOrWhiteSpace(fieldName)) continue;
                rowHeads.Add(new RowHead(fieldName, i));
                data.Columns.Add(fieldName, fieldType);
                //data.Columns.Add(fieldName);
            }
            if (rowHeads.Count == 0) return null;

            for (int i = _setting.StartRowNum; i < sheet.Rows.Count; i++)
            {
                DataRow row = data.NewRow();
                for (int j = 0; j < rowHeads.Count; j++)
                {
                    object value = sheet.Rows[i][rowHeads[j].index];
                    row[j] = value;
                }
                data.Rows.Add(row);
            }

            return data;
        }


        public void SetSetting(ISetting setting)
        {
            _setting = (SqliteSetting)setting;
        }

        public ISetting GetSetting()
        {
            return _setting;
        }
    }
}
