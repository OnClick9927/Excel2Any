using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{
    public class SqliteSetting : BaseSetting
    {
        /// <summary>
        /// 字段名行号设置
        /// </summary>
        public int FieldRowNum;

        /// <summary>
        /// 字段类型行号设置
        /// </summary>
        public int TypeRowNum;

        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        public int StartRowNum;

        public SqliteSetting() : base()
        {
            TypeRowNum = 1;                 //字段类型所在行号
            FieldRowNum = 0;                //字段名所在行号
            StartRowNum = 3;                //内容开始的行号
        }
    }
}
