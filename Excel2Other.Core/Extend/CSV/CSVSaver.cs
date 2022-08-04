namespace Excel2Other
{
    [Entity(typeof(CSVEntity))]
    public class CSVSaver : TextSaver
    {

        public CSVSaver(ISetting setting) : base(setting)
        {
            extension = "csv";
        }
    }
}
