using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Excel2Any
{
    public abstract class Entity
    {
        private IConverter _converter;
        private IConverterSaver _save;
        public IConverter converter => _converter;
        public IConverterSaver save => _save;
        public Entity(IConverter c, IConverterSaver save)
        {
            _save = save;
            _converter = c;
        }
        public void SetSetting(ISetting set)
        {
            converter.SetSetting(set);
            save.SetSetting(set);
        }
        public ISetting GetSetting()
        {
            return converter.GetSetting();
        }
        public List<SheetData> Convert(RawDataDetail rawData)
        {
            return converter?.Convert(rawData);
        }
        public string Save2String(List<SheetData> sheets)
        {
            return save.Save2String(sheets);
        }

        /// <summary>
        /// 将内容保存到文件
        /// </summary>
        /// <param name="sheets">内容</param>
        /// <param name="fileName">保存数据库的时候需要填FileName，保存文字的时候不填，填了也么用</param>
        public void Save2File(List<SheetData> sheets, string fileName = "")
        {
            save.Save2File(sheets, fileName);
        }
    }
}
