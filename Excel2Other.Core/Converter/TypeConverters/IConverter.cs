using System.Collections.Generic;
using System.Data;

namespace Excel2Other
{
    public interface IConverter
    {
        /// <summary>
        /// 生成的扩展名
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// 每个Sheet的名字和内容
        /// </summary>
        List<SheetContent> Sheets { get; }

        /// <summary>
        /// 将DataSet转换成要显示出来的字符串
        /// </summary>
        /// <param name="data">数据</param>
        void Convert(DataSet data);

        /// <summary>
        /// 传入设置
        /// </summary>
        /// <param name="setting">设置</param>
        void SetSetting(ISettings setting);

        /// <summary>
        /// 获取当前设置
        /// </summary>
        /// <returns>设置</returns>
        ISettings GetSetting();
    }
}
