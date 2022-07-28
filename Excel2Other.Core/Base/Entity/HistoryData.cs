using System;
using System.Collections.Generic;
using System.Data;

namespace Excel2Other
{
    public class HistoryData
    {
        /// <summary>
        /// 文件的上一次修改时间，判断文件是否修改用
        /// </summary>
        public DateTime lastWriteTime;

        /// <summary>
        /// 数据
        /// </summary>
        public DataSet data;
        public HistoryData(DateTime writeTime,DataSet data)
        {
            this.lastWriteTime = writeTime;
            this.data = data;
        }
    }
}
