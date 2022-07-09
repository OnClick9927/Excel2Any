using System.Collections.Generic;

namespace Excel2Other.Core.__Interface
{
    public interface IConverterSaver
    {
        string Save2String(List<SheetData> sheets);
        void Save2File(string path, List<SheetData> sheets);

    }
}
