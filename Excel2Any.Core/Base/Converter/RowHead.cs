using System;

namespace Excel2Any
{

    /// <summary>
    /// 用于保存表头 索引 描述 类型
    /// </summary>
    public struct RowHead
    {
        public string fieldName;
        public int index;
        public string typeName;
        public string comment;

        public RowHead(string rowName, int index,string typeName, string comment)
        {
            this.fieldName = rowName;
            this.index = index;
            this.typeName = typeName;
            this.comment = comment;
        }
    }
}
