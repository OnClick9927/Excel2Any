using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{
    public class TextSaver : IConverterSaver
    {
        static Encoding encoding = new UTF8Encoding(false);
        public TextSaver(ISetting setting) : base(setting)
        {
        }

        public override void Save2File(List<SheetData> sheets, string fileName)
        {
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

            for (int i = 0; i < sheets.Count; i++)
            {
                var filePath = $"{savePath}/{sheets[i].sheetName}.{extension}";
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (TextWriter writer = new StreamWriter(file, encoding))
                        writer.Write(sheets[i].content);
                }
            }
        }

        public override string Save2String(List<SheetData> sheets)
        {
            if (sheets != null && sheets.Count > 0)
            {
                return sheets[0].content.ToString();
            }
            return string.Empty;
        }
    }
}
