using System.Collections.Generic;

namespace Excel2Any
{
    public abstract class IConverterSaver
    {
        private ISetting _setting;
        protected ISetting setting => _setting;

        /// <summary>
        /// 扩展名
        /// </summary>
        protected string extension;
        public IConverterSaver(ISetting setting)
        {
            this._setting = setting;
        }
        public abstract string Save2String(List<SheetData> sheets);
        public abstract void Save2File(List<SheetData> sheets, string fileName);
        /// <summary>
        /// 传入设置
        /// </summary>
        /// <param name="setting">设置</param>
        public void SetSetting(ISetting setting)
        {
            this._setting = setting;
        }
    }
}
