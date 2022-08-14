
using Newtonsoft.Json;
using System;

namespace Excel2Any
{
    public  class DataValueUtil
    {
        /// <summary>
        /// 去掉数值字段的“.0”
        /// </summary>
        /// <param name="value">传入值</param>
        /// <returns>传出值</returns>
        public static object GetIntValue(object value)
        {
            if (value.GetType() == typeof(double))
            {
                double num = (double)value;
                if ((int)num == num)
                    value = (int)num;
            }
            return value;
        }

        /// <summary>
        /// 解析为json
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetJsonObj(object value)
        {
            string cellText = value.ToString().Trim();
            if (cellText.StartsWith("[") || cellText.StartsWith("{"))
            {
                try
                {
                    if (cellText.EndsWith(","))
                    {
                        cellText = cellText.Substring(0, cellText.Length - 1);
                    }
                    object cellJsonObj = JsonConvert.DeserializeObject(cellText);
                    if (cellJsonObj != null)
                        value = cellJsonObj;
                }
                catch (Exception)
                {
                    
                }

            }

            return value;
        }
    }
}
