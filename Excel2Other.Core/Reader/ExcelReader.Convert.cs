using System;
using System.Collections.Generic;

namespace Excel2Other
{
	public partial class ExcelReader
	{
		Dictionary<ConvertType, IConverter> _converters = new Dictionary<ConvertType, IConverter>();

		/// <summary>
		/// 更新转换内容
		/// </summary>
		public void UpdateContent()
		{
			foreach (var converter in _converters)
			{
				converter.Value.Convert(_data);
			}
		}

		/// <summary>
		/// 传入CSharp设置对象
		/// </summary>
		public void SetSetting(CSharpSettings settings)
		{
            if (_converters.ContainsKey(ConvertType.CSharp))
            {
				_converters[ConvertType.CSharp].SetSetting(settings);
			}
            else
            {
				_converters.Add(ConvertType.CSharp, new CSharpConverter(settings));
			}
		}

		/// <summary>
		/// 传入CSharp设置对象
		/// </summary>
		public void SetSetting(JsonSettings settings)
		{
			if (_converters.ContainsKey(ConvertType.Json))
			{
				_converters[ConvertType.Json].SetSetting(settings);
			}
			else
			{
				_converters.Add(ConvertType.Json, new JsonConverter(settings));
			}
		}

		/// <summary>
		/// 传入CSharp设置对象
		/// </summary>
		public void SetSetting(XmlSettings settings)
		{
			if (_converters.ContainsKey(ConvertType.Xml))
			{
				_converters[ConvertType.Xml].SetSetting(settings);
			}
			else
			{
				_converters.Add(ConvertType.Xml, new XmlConverter(settings));
			}
		}


		public List<SheetContent> GetSheets(ConvertType type)
		{
			return _converters[type].Sheets;
		}
	}
}
