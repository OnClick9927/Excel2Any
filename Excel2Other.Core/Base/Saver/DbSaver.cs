using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{ 
    public  class DbSaver : IConverterSaver
    {
        public DbSaver(ISetting setting) : base(setting)
        {
        }

        public override void Save2File(List<SheetData> sheets, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            //拼出路径，判断路径是否存在
            if (sheets == null || sheets.Count == 0) return;

            var setting = (BaseSetting)this.setting;

            var savePath = setting.savePath;
            if (string.IsNullOrEmpty(savePath))
            {
                savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            //文件存在则删除
            var filePath = $"{savePath}/{fileName}.{extension}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            try
            {
                SQLiteConnection.CreateFile(filePath);
                string sqliteConnection = $"Data Source = {filePath}";
                SQLiteHelper.ExecuteNonQuery(sqliteConnection, Save2String(sheets));
            }
            catch (Exception ex)
            {
                throw new Exception("新建数据库文件" + filePath + "失败：" + ex.Message);
            }


        }

        public override string Save2String(List<SheetData> sheets)
        {

            if (sheets == null || sheets.Count == 0) return "";
            StringBuilder totalQuery = new StringBuilder();
            foreach (var sheetData in sheets)
            {
                //创建表，遍历表头获取标题
                StringBuilder sb = new StringBuilder();
                sb.Append($"Create table { sheetData.sheetName }");
                sb.Append("(");
                var content = (DataContent)sheetData.content;
                var table = content.value;
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sb.Append($"{table.Columns[i].ColumnName} {FieldTypeUtil.GetSqliteType(table.Columns[i].DataType)},");
                }
                sb.Append(");");

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    StringBuilder rows = new StringBuilder();
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        rows.Append($"{table.Rows[i][j]},");
                    }
                    sb.AppendLine($"insert into {sheetData.sheetName} values({rows})");
                }
                totalQuery.Append(sb);
            }
            return totalQuery.ToString();
        }
    }
}
