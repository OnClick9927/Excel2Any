﻿using System.Collections.Generic;
using System.Data;

namespace Excel2Other
{
    public class DataContent :IContent
    {
        public DataTable value;

        public DataContent(DataTable value)
        {
            this.value = value;
        }
    }
}
