using System;
using System.Collections.Generic;
using System.Data;

namespace Excel2Any
{
    public class RawDataDetail
    {
        /// <summary>
        /// 文件的crc值
        /// </summary>
        public ushort crc;

        /// <summary>
        /// 数据
        /// </summary>
        public DataSet data;

        public List<List<RowHead>> headsCollection;
        public RawDataDetail(ushort crc,DataSet data, List<List<RowHead>> headsCollection)
        {
            this.crc = crc;
            this.data = data;
            this.headsCollection = headsCollection;
        }
    }
}
