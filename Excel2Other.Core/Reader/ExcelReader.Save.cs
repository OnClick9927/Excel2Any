using System;
using System.IO;
using System.Text;

namespace Excel2Other
{
    public partial class ExcelReader
    {
        Encoding encoding = new UTF8Encoding(false);

        public void Save(ConvertType type)
        {
            
            var converter = _converters[type];
            if (converter.Sheets == null || converter.Sheets.Count == 0) return;

            var setting = (BaseSettings)converter.GetSetting();

            //var savePath = $"{setting.savePath}/{type}";
            var savePath = setting.savePath;
            if (string.IsNullOrEmpty(savePath))
            {
                savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            for (int i = 0; i < converter.Sheets.Count; i++)
            {
                var filePath = $"{savePath}/{converter.Sheets[i].sheetName}.{converter.Extension}";
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (TextWriter writer = new StreamWriter(file, encoding))
                        writer.Write(converter.Sheets[i].content);
                }
            }

        }

        /// <summary>
        /// 保存C#脚本
        /// </summary>
        public void SaveCSharp()
        {
            Save(ConvertType.CSharp);
        }

        /// <summary>
        /// 保存Json
        /// </summary>
        public void SaveJson()
        {
            Save(ConvertType.Json);
        }

        /// <summary>
        /// 保存Xml
        /// </summary>
        public void SaveXml()
        {
            Save(ConvertType.Xml);
        }
    }
}
