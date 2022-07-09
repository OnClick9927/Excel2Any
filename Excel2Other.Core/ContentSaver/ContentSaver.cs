using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{
    public class ContentSaver
    {
        static Encoding encoding = new UTF8Encoding(false);
        public static void SaveToText(IContent content,ISetting settings)
        {
            //if (converter.Sheets == null || converter.Sheets.Count == 0) return;

            //var setting = (BaseSettings)converter.GetSetting();

            ////var savePath = $"{setting.savePath}/{type}";
            //var savePath = setting.savePath;
            //if (string.IsNullOrEmpty(savePath))
            //{
            //    savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //}

            //if (!Directory.Exists(savePath))
            //{
            //    Directory.CreateDirectory(savePath);
            //}

            //for (int i = 0; i < converter.Sheets.Count; i++)
            //{
            //    var filePath = $"{savePath}/{converter.Sheets[i].sheetName}.{converter.Extension}";
            //    using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            //    {
            //        using (TextWriter writer = new StreamWriter(file, encoding))
            //            writer.Write(converter.Sheets[i].content);
            //    }
            //}
        }

        public static void SaveToDB(IContent content, ISetting settings)
        {
            
        }

    }
}
