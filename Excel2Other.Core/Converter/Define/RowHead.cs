namespace Excel2Other
{

    /// <summary>
    /// 用于保存表头和索引
    /// </summary>
    public struct RowHead
    {
        public string fieldType;
        public string fieldName;
        public int index;

        public RowHead(string rowName, int index)
        {
            fieldType = null;
            this.fieldName = rowName;
            this.index = index;
        }
        public RowHead(string rowName, string type ,int index)
        {
            fieldType = type;
            this.fieldName = rowName;
            this.index = index;
        }
    }
}
