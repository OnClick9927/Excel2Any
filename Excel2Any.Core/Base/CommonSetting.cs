using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any
{
    public class CommonSetting : ISetting
    {
        /// <summary>
        /// 排除前缀
        /// </summary>
        [SettingAttribute(
            "排除字符",
            "在读取时带排除字符的Sheet名",
            priority = 120
            )]
        public string excludeSheetString;
        /// <summary>
        /// 是否排除sheet
        /// </summary>
        [SettingAttribute(
            "根据前缀排除Sheet",
            "读取Excel文件时，跳过带排除字符的Sheet名",
            priority = 130
            )]
        public bool excludeSheet;

        /// <summary>
        /// 是否排除第一列（第一列第一格为sheet名）
        /// </summary>
        [SettingAttribute(
            "以第一行第一列单元格为Sheet名",
            "以第一个单元格为Sheet名，并排除第一列所有内容",
            priority = 140
            )]
        public bool excludeFirstCol;

        /// <summary>
        /// 描述行号设置
        /// </summary>
        [SettingAttribute(
            "字段描述行号",
            "Excel表中对应字段描述的行号（从1开始）",
            priority = 150
            )]
        public int CommentRowNum;

        /// <summary>
        /// 字段名行号设置
        /// </summary>
        [SettingAttribute(
            "字段名行号",
            "Excel表中对应字段名的行号（从1开始）",
            priority = 160
            )]
        public int FieldRowNum;

        /// <summary>
        /// 字段类型行号设置
        /// </summary>
        [SettingAttribute(
            "字段类型行号",
            "Excel表中对应字段类型的行号（从1开始）",
            priority = 170
            )]
        public int TypeRowNum;

        /// <summary>
        /// 内容行开始的行数
        /// </summary>
        [SettingAttribute(
            "内容开始行号",
            "Excel表中对应内容开始的行号（从1开始）",
            priority = 180
            )]
        public int StartRowNum;

        public CommonSetting()
        {
            excludeSheetString = "#";
            excludeSheet = true;
            excludeFirstCol = false;
            //注意这个实际保存不是从1开始，上面从1开始是为了对应excel的行数
            CommentRowNum = 0;
            FieldRowNum = 1;
            TypeRowNum = 2;
            StartRowNum = 3;
        }
    }
}
