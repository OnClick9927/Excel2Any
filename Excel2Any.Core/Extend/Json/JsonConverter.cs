using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Excel2Any
{
    [Entity(typeof(JsonEntity))]
    public class JsonConverter : IConverter
    {
        //用于默认值的保存
        private Dictionary<int, object> _defaultValue = new Dictionary<int, object>();
        //Json配置
        private JsonSerializerSettings _jsonSerialSettings;
        //转换设置
        private JsonSetting _setting;

        /// <summary>
        /// Ctor
        /// </summary>
        public JsonConverter(ISetting setting)
        {
            _jsonSerialSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            _setting = (JsonSetting)setting;
        }

        public List<SheetData> Convert(RawDataDetail rawData)
        {
            List<SheetData> allSheetData = new List<SheetData>();
            Dictionary<string, List<object>> sheetDataDic = new Dictionary<string, List<object>>();
            _defaultValue.Clear();

            //更改设置
            _jsonSerialSettings.DateFormatString = _setting.dateFormat;
            var data = rawData.data;
            for (int i = 0; i < data.Tables.Count; i++)
            {
                var sheet = data.Tables[i];
                var rowHeadList = rawData.headsCollection[i];
                var sheetName = sheet.TableName;
                var sheetData = ConvertSheet(sheet, rowHeadList);

                //判断是否拆分
                if (_setting.separateBySheet)
                {
                    if (sheetData != null)
                    {
                        var content = JsonConvert.SerializeObject(sheetData, _jsonSerialSettings);
                        allSheetData.Add(new SheetData(sheetName, new TextContent(content)));
                    }
                }
                else
                {
                    sheetDataDic.Add(sheetName, sheetData);
                }
            }

            //判断是否拆分
            if (!_setting.separateBySheet)
            {
                if (sheetDataDic.Count > 0)
                {
                    var content = JsonConvert.SerializeObject(sheetDataDic, _jsonSerialSettings);
                    allSheetData.Add(new SheetData(data.DataSetName, new TextContent(content)));
                }

            }
            return allSheetData;
        }

        private List<object> ConvertSheet(DataTable sheet, List<RowHead> rowHeads)
        {

            //保存每行的数据
            List<object> rows = new List<object>();
            //遍历每行根据表头转换成对象字典
            for (int i = 0; i < sheet.Rows.Count; i++)
            {
                //获取列的名
                var rowDict = convertRowToDict(sheet, rowHeads, i);
                if (rowDict.Count > 0)
                {
                    rows.Add(rowDict);
                }
            }

            return rows;
        }

        /// <summary>
        /// 把一行数据转换成一个对象，每一列是一个属性
        /// </summary>
        private Dictionary<string, object> convertRowToDict(DataTable sheet, List<RowHead> rowHeads, int rowIndex)
        {
            var rowData = new Dictionary<string, object>();

            //根据表头获取对应的属性
            for (int i = 0; i < rowHeads.Count; i++)
            {
                string fieldName = rowHeads[i].fieldName;
                object value = sheet.Rows[rowIndex][i];

                #region 对单元格进行处理
                //为空时的处理
                if (value.GetType() == typeof(System.DBNull) && string.IsNullOrWhiteSpace(value.ToString()))
                {
                    if (_setting.saveSpace)
                    {
                        value = GetColumnDefault(sheet, i, _setting.StartRowNum);
                    }
                    else
                    {
                        continue;
                    }
                }
                //对单元格内Json的处理
                else if (_setting.JsonCell)
                {
                    value = DataValueUtil.GetJsonObj(value);
                }

                value = DataValueUtil.GetIntValue(value);

                //全部转换为string
                if (_setting.allString && !(value is string))
                {
                    value = value.ToString();
                }
                #endregion
                rowData[fieldName] = value;
            }

            return rowData;
        }

        /// <summary>
        /// 根据同一列的非空值获取默认值，并构造一个同类型的默认值
        /// 使用字典做性能优化
        /// </summary>
        private object GetColumnDefault(DataTable sheet, int column, int startRowNum)
        {
            if (_defaultValue.ContainsKey(column))
            {
                return _defaultValue[column];
            }
            for (int i = startRowNum; i < sheet.Rows.Count; i++)
            {
                object cellValue = sheet.Rows[i][column];
                Type valueType = cellValue.GetType();
                if (valueType != typeof(System.DBNull))
                {
                    if (valueType.IsValueType)
                    {
                        var defaltValue = Activator.CreateInstance(valueType);
                        _defaultValue[column] = defaltValue;
                        return defaltValue;
                    }
                    break;
                }
            }

            //非值类型就返回空字符串
            _defaultValue[column] = "";
            return "";
        }

        public void SetSetting(ISetting setting)
        {
            _setting = (JsonSetting)setting;
        }

        public ISetting GetSetting()
        {
            return _setting;
        }
    }
}
