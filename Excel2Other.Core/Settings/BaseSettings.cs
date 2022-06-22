using System.Collections.Generic;

namespace Excel2Other
{
    public class BaseSettings : ISettings
    {
        /// <summary>
        /// 保存时的路径
        /// </summary>
        public string savePath;
        /// <summary>
        /// 是否保存在同一个文件夹
        /// </summary>
        public bool saveInSamePath;

        /// <summary>
        /// 是否将Sheet拆开保存
        /// </summary>
        public bool separateBySheet;

        /// <summary>
        /// 排除前缀
        /// </summary>
        public string excludePrefix;
        /// <summary>
        /// 是否排除sheet
        /// </summary>
        public bool excludeSheet;


        /// <summary>
        /// 是否排除第一列（第一列第一格为sheet名）
        /// </summary>
        public bool excludeFirstCol;

        /// <summary>
        /// Ctor
        /// </summary>
        public BaseSettings()
        {
            savePath = "";                  //保存路径
            separateBySheet = true;         //是否根据Sheet保存成多个文件
            excludePrefix = "#";             //用于排除的前缀字符串
            excludeSheet = true;            //是否根据前缀字符串排除Sheet


            excludeFirstCol = true;        //排除第一列（第一列第一行写sheet名）
        }
    }
}
