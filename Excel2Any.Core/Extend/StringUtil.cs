using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    public class StringUtil
    {
        /// <summary>
        /// 路径格式化
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>格式化后的路径</returns>
        public static string PathFormatter(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                return fileInfo.FullName;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }



}
