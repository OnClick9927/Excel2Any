using System;
using System.IO;
using System.Text;

namespace Excel2Other
{
    public partial class ExcelReader
    {

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="path">读取的路径</param>
        /// <param name="type">保存的类型</param>
        public void Save(string path,ConvertType type)
        {
            
            //var converter = _converters[type];
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
    }
}
