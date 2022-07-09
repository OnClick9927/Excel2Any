using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace Excel2Other
{
    public partial class ExcelReader : IExcelReader
    {
        /// <summary>
        /// 文件路径  历史读取的DataSet数据
        /// </summary>
        Dictionary<string, HistoryData> _historyData;

        ExcelDataTableConfiguration tableConfig;
        public static ExcelDataSetConfiguration dataSetConfig;

        /// <summary>
        /// Ctor
        /// </summary>
        public ExcelReader()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            _historyData = new Dictionary<string, HistoryData>();
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
        private void Read(string filePath)
        {
            if (!File.Exists(filePath)) return;

            FileInfo fileInfo = new FileInfo(filePath);
            DateTime lastWriteTime = fileInfo.LastWriteTime;
            string fullPath = fileInfo.FullName;
            //如果包含相同的缓存则不读取,同一规定FileInfo拿地址防止格式不同
            if (_historyData.ContainsKey(fullPath) && _historyData[fullPath].lastWriteTime == lastWriteTime) return;

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(dataSetConfig);
                        result.DataSetName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));
                        var hisData = new HistoryData(fullPath, lastWriteTime, result);
                        _historyData[fullPath] = hisData;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
