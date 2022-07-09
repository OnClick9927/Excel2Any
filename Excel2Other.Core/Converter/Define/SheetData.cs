namespace Excel2Other
{
    /// <summary>
    /// 保存每一个Sheet的名字和转化出来的内容
    /// </summary>
    public class SheetData
    {
        public string sheetName;
        public IContent content;

        public SheetData(string sheetName,IContent content)
        {
            this.sheetName = sheetName;
            this.content = content;
        }
    }
}
