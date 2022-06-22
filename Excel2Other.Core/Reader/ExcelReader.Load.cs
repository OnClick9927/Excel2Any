using System;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace Excel2Other
{
    public partial class ExcelReader : IExcelReader
    {
        private DataSet _data;
        ExcelDataTableConfiguration tableConfig;
        ExcelDataSetConfiguration dataSetConfig;
        /// <summary>
        /// Ctor
        /// </summary>
        public ExcelReader()
        {
            tableConfig = new ExcelDataTableConfiguration();
            dataSetConfig = new ExcelDataSetConfiguration();
            dataSetConfig.UseColumnDataType = true;
            dataSetConfig.ConfigureDataTable = (_) => { return tableConfig; };
        }

        /// <summary>
        /// 读取excel到DataSet
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取是否成功</returns>
        public bool Read(string filePath)
        {
            _data = null;
            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(dataSetConfig);
                        this._data = result;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }


            if (this._data != null)
            {
                _data.DataSetName = Path.GetFileNameWithoutExtension(filePath);
                UpdateContent();
                return true;
            }
            return false;
        }

    }
}
