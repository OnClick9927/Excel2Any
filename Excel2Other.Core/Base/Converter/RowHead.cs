namespace Excel2Other
{

    /// <summary>
    /// 用于保存表头和索引
    /// </summary>
    public struct RowHead
    {
        public string fieldName;
        public int index;

        public RowHead(string rowName, int index)
        {
            this.fieldName = rowName;
            this.index = index;
        }
    }
}
