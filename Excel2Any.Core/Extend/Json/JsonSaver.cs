namespace Excel2Any
{
    [Entity(typeof(JsonEntity))]
    public class JsonSaver : TextSaver
    {
        
        public JsonSaver(ISetting setting) : base(setting)
        {
            extension = "json";
        }
    }
}
