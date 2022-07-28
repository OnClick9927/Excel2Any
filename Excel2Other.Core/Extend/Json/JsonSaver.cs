using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;

namespace Excel2Other
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
