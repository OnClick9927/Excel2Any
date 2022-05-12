namespace excel2other
{
    /// <summary>
    /// 命令行参数定义
    /// </summary>
    public class Options
    {
        public Options()
        {
            this.HeaderRows = 3;
            this.Encoding = "utf8-nobom";
            this.Lowcase = false;
            this.ExportArray = false;
            this.ForceSheetName = false;
        }

        /// <summary>
        /// 读取路径
        /// </summary>
        public string InPath { get; set; }

        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// number lines in sheet as header
        /// </summary>
        public int HeaderRows { get; set; }

        /// <summary>
        /// export file encoding
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// convert filed name to lowcase
        /// </summary>
        public bool Lowcase { get; set; }

        /// <summary>
        /// export as array, otherwise as dict object
        /// </summary>
        public bool ExportArray { get; set; }

        /// <summary>
        /// Date Format String
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// export with sheet name, even there's only one sheet
        /// </summary>
        public bool ForceSheetName { get; set; }

        /// <summary>
        /// exclude sheet or column start with specified prefix
        /// </summary>
        public string ExcludePrefix { get; set; }

        /// <summary>
        /// convert json string in cell
        /// </summary>
        public bool CellJson { get; set; }

        /// <summary>
        /// all string
        /// </summary>
        public bool AllString { get; set; }

        /// <summary>
        /// 从配置文件中读取Option
        /// </summary>
        /// <param name="path">Ini路径</param>
        public static Options LoadIni(string path)
        {
            var ini = new IniFile();
            var options = new Options();
            if (string.IsNullOrEmpty(path))
                path = "config.ini";
            ini.Load(path);

            options.ExportArray = ini["App"]["ExportArray"].ToBool(true);

            options.Encoding = ini["App"]["Encoding"].GetString();
            if (string.IsNullOrEmpty(options.Encoding))
            {
                options.Encoding = "utf8-nobom";
            }
            options.DateFormat = ini["App"]["DateFormat"].GetString();
            if (string.IsNullOrEmpty(options.DateFormat))
            {
                options.DateFormat = "yyyy/MM/dd";
            }

            options.Lowcase = ini["App"]["Lowcase"].ToBool(false);
            options.HeaderRows = ini["App"]["HeaderRows"].ToInt(3);
            options.ForceSheetName = ini["App"]["ForceSheetName"].ToBool(false);
            options.ExcludePrefix = ini["App"]["ExcludePrefix"].GetString();
            options.CellJson = ini["App"]["CellJson"].ToBool(false);
            options.AllString = ini["App"]["AllString"].ToBool(false);

            options.OutputPath = ini["App"]["OutputPath"].GetString();
            options.InPath = ini["App"]["InPath"].GetString();

            return options;
        }

        public static void SaveINI(Options options, string path = "")
        {
            var ini = new IniFile();

            if (!string.IsNullOrEmpty(options.Encoding))
            {
                ini["App"]["Encoding"] = options.Encoding;
            }

            if (!string.IsNullOrEmpty(options.DateFormat))
            {
                ini["App"]["DateFormat"] = options.DateFormat;
            }

            ini["App"]["OutputPath"] = options.OutputPath;
            ini["App"]["InPath"] = options.InPath;

            ini["App"]["ExcludePrefix"] = options.ExcludePrefix;
            ini["App"]["ExportArray"] = options.ExportArray;
            ini["App"]["Lowcase"] = options.Lowcase;
            ini["App"]["HeaderRows"] = options.HeaderRows;
            ini["App"]["ForceSheetName"] = options.ForceSheetName;
            ini["App"]["CellJson"] = options.CellJson;
            ini["App"]["AllString"] = options.AllString;

            if (string.IsNullOrEmpty(path))
            {
                path = "config.ini";
            }
            ini.Save(path);
        }
    }

    /*
    * * -e, –excel Required. 输入的Excel文件路径.
    * -j, –json 指定输出的json文件路径.
    * -h, –header Required. 表格中有几行是表头.
    * -c, –encoding (Default: utf8-nobom) 指定编码的名称.
    * -l, –lowcase (Default: false) 自动把字段名称转换成小写格式.
    * -a 序列化成数组
    * -d, --date:指定日期格式化字符串，例如：dd / MM / yyy hh: mm:ss
    * -s 序列化时强制带上sheet name，即使只有一个sheet
    * -exclude_prefix： 导出时，排除掉包含指定前缀的表单和列，例如：-exclude_prefix #
    * -cell_json：自动识别单元格中的Json对象和Json数组，Default：false
     */
}
