using System;
using System.Collections.Generic;

namespace Excel2Other
{
	public partial class ExcelReader
	{
		Dictionary<ConvertType, IConverter> _converters = new Dictionary<ConvertType, IConverter>();

		/// <summary>
		/// 根据设置创建转换器
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="settings">设置</param>
		public void CreateConverter(ConvertType type,ISetting settings)
		{
            if (_converters.ContainsKey(type))
            {
				_converters[type].SetSetting(settings);

                //清除读取的缓存
                foreach (var historyData in _historyData)
                {
					historyData.Value.convertedData.Remove(type);
				}
			}
            else
            {
				_converters.Add(type, ConvertFactory.CreateConverter(type,settings));
			}
		}

		/// <summary>
		/// 从缓存中获取内容
		/// 如果缓存不存在则创建
		/// </summary>
		/// <param name="path">路径</param>
		/// <param name="type">类型</param>
		/// <returns>转换后的内容</returns>
		public List<SheetData> GetContent(string path,ConvertType type)
		{
			var formattedPath = StringUtil.PathFormatter(path);
			//没有读取过就读入缓存
			if (!_historyData.ContainsKey(formattedPath))
            {
				Read(path);
            }

			//没有转换的缓存就生成缓存
            if (!_historyData[formattedPath].convertedData.ContainsKey(type))
            {
				//不存在转换器就报错
				if (!_converters.ContainsKey(type))
				{
					throw new Exception($"缺少{type}转换器");
				}
                else
                {
					_historyData[StringUtil.PathFormatter(path)].convertedData[type] = _converters[type].Convert(_historyData[formattedPath].data);
				}
			}

			return _historyData[StringUtil.PathFormatter(path)].convertedData[type];
		}


		/// <summary>
		/// 清除缓存
		/// </summary>
		public void Clear()
		{
			_historyData.Clear();
		}

		/// <summary>
		/// 根据转换类型清除缓存
		/// </summary>
		/// <param name="type"></param>
		public void Clear(ConvertType type)
		{
            foreach (var data in _historyData)
            {
                if (data.Value.convertedData.ContainsKey(type))
                {
					data.Value.convertedData.Remove(type);

				}
            }
		}
	}
}