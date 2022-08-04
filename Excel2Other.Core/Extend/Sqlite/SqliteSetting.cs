using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other
{
    [Entity(typeof(SqliteEntity))]
    public class SqliteSetting : BaseSetting
    {

        /// <summary>
        /// 字段名行号设置
        /// </summary>
        [SettingAttribute(
            "字段名行号",
            "Excel表中对应字段名的行号（从1开始）",
            priority = 100
            )]
        public int FieldRowNum;

        /// <summary>
        /// 字段类型行号设置
        /// </summary>
        [SettingAttribute(
            "字段类型行号",
            "Excel表中对应字段类型的行号（从1开始）",
            priority = 101
            )]
        public int TypeRowNum;

        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        [SettingAttribute(
            "内容开始行号",
            "Excel表中对应内容开始的行号（从1开始）",
            priority = 102
            )]
        public int StartRowNum;

        public SqliteSetting() : base()
        {
            TypeRowNum = 2;                 //字段类型所在行号
            FieldRowNum = 1;                //字段名所在行号
            StartRowNum = 4;                //内容开始的行号
        }
    }
}
