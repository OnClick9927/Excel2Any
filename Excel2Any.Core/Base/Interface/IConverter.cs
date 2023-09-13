using System.Collections.Generic;
using System.Data;

namespace Excel2Any
{
    /// <summary>
    /// 转换器只支持转换，不保存任何数据
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// 将DataSet转换成对应内容
        /// </summary>
        /// <param name="data">数据</param>
        List<SheetData> Convert(RawDataDetail data);

        /// <summary>
        /// 传入设置
        /// </summary>
        /// <param name="setting">设置</param>
        void SetSetting(ISetting setting);

        /// <summary>
        /// 获取当前设置
        /// </summary>
        /// <returns>设置</returns>
        ISetting GetSetting();
    }
}
