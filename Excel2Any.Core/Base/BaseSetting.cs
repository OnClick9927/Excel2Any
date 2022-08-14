using System;
using System.Collections.Generic;
using System.Reflection;

namespace Excel2Any
{
    public class BaseSetting : ISetting
    {
        /// <summary>
        /// 保存时的路径
        /// </summary>
        [SettingAttribute(
            "保存路径",
            "保存时的默认路径，为空时默认在桌面",
            textType = StringType.Directory,
            priority = 1
            )]
        public string savePath;

        /// <summary>
        /// 排除前缀
        /// </summary>
        [SettingAttribute(
            "排除前缀",
            "在读取时排除以特定字符开头的Sheet名",
            priority = 2
            )]
        public string excludePrefix;
        /// <summary>
        /// 是否排除sheet
        /// </summary>
        [SettingAttribute(
            "根据前缀排除Sheet",
            "读取Excel文件时，跳过带排除前缀的Sheet名",
            priority = 3
            )]
        public bool excludeSheet;

        /// <summary>
        /// 是否排除第一列（第一列第一格为sheet名）
        /// </summary>
        [SettingAttribute(
            "以第一行第一列单元格为Sheet名",
            "以第一个单元格为Sheet名，并排除第一列所有内容",
            priority = 4
            )]
        public bool excludeFirstCol;

        /// <summary>
        /// Ctor
        /// </summary>
        public BaseSetting()
        {
            savePath = "";                  //保存路径
            excludePrefix = "#";             //用于排除的前缀字符串
            excludeSheet = true;            //是否根据前缀字符串排除Sheet
            excludeFirstCol = true;        //排除第一列（第一列第一行写sheet名）
        }

    }
}
