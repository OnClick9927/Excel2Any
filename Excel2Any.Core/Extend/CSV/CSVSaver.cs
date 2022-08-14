namespace Excel2Any
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
