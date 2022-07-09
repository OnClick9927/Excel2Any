using System;
using System.Collections.Generic;
using System.Data;

namespace Excel2Other
{
    public struct HistoryData
    {
        /// <summary>
        /// 读取的文件名
        /// </summary>
        public string fileName;

        /// <summary>
        /// 文件的上一次修改时间，判断文件是否修改用
        /// </summary>
        public DateTime lastWriteTime;

        /// <summary>
        /// 数据
        /// </summary>
        public DataSet data;

        /// <summary>
        /// 之前转换过的内容留存
        /// </summary>
        public Dictionary<ConvertType, List<SheetData>> convertedData;
        public HistoryData(string fileName,DateTime writeTime,DataSet data)
        {
            this.fileName = fileName;
            this.lastWriteTime = writeTime;
            this.data = data;

            convertedData = new Dictionary<ConvertType, List<SheetData>>();
        }
    }
}
