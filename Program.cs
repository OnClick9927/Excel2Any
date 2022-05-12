using System;
using System.IO;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace excel2other
{
    /// <summary>
    /// 应用程序
    /// </summary>
    sealed partial class Program
    {
        /// <summary>
        /// 应用程序入口
        /// </summary>
        /// <param name="args">命令行参数</param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.MainForm());
        }
    }
}
