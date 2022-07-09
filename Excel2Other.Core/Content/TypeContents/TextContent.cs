namespace Excel2Other
{
    public class TextContent : IContent
    {
        public string value;
        public TextContent() { }
        public TextContent(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
