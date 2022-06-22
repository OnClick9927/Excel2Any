namespace Excel2Other
{
    /// <summary>
    /// 保存每一个Sheet的名字和转化出来的内容
    /// </summary>
    public class SheetContent
    {
        public string sheetName;
        public string content;

        public SheetContent() { }
        public SheetContent(string sheetName,string content)
        {
            this.sheetName = sheetName;
            this.content = content;
        }
    }
}
