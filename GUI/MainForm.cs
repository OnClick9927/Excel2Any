using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace excel2other.GUI
{
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class MainForm : Form
    {
        // Excel导入数据管理
        private DataManager mDataMgr;

        // 支持语法高亮的文本框
        private FastColoredTextBox mJSONTextBox;
        private FastColoredTextBox mCSharpTextBox;
        private FastColoredTextBox mXMLTextBox;

        // 文本框的样式
        private TextStyle mBrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        private TextStyle mMagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        private TextStyle mGreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);

        // 导出数据相关的按钮，方便整体Enable/Disable
        private List<Button> mExportButtonList;

        /// <summary>
        /// 构造函数，初始化控件初值；创建文本框
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //生成高亮文本框
            mJSONTextBox = CreateTextBoxInTab(this.tabPageJSON);
            mJSONTextBox.Language = Language.Custom;
            mJSONTextBox.TextChanged += new EventHandler<TextChangedEventArgs>(this.JsonTextChanged);

            mCSharpTextBox = CreateTextBoxInTab(this.tabCSharp);
            mCSharpTextBox.Language = Language.CSharp;

            mXMLTextBox = CreateTextBoxInTab(this.tabXML);
            mXMLTextBox.Language = Language.XML;

            //将按钮放入集合方便管理
            mExportButtonList = new List<Button>();
            mExportButtonList.Add(this.btnCopyJSON);
            mExportButtonList.Add(this.btnSaveJSON);
            mExportButtonList.Add(this.btnCopyCSharp);
            mExportButtonList.Add(this.btnSaveCSharp);
            mExportButtonList.Add(this.btnCopyXML);
            mExportButtonList.Add(this.btnSaveXML);
            //禁用按键
            EnableExportButtons(false);

            //清空列表
            lvwData.Items.Clear(); ;

            //-- data manager
            mDataMgr = new DataManager();
            //打开直接读取
            if (!string.IsNullOrEmpty(mDataMgr.options.InPath))
            {
                LoadFolder(mDataMgr.options.InPath);
            }
        }

        /// <summary>
        /// 设置导出相关的按钮是否可用
        /// </summary>
        /// <param name="enable">是否可用</param>
        private void EnableExportButtons(bool enable)
        {
            foreach (var btn in mExportButtonList)
                btn.Enabled = enable;
        }

        /// <summary>
        /// 在一个TabPage中创建Text Box
        /// </summary>
        /// <param name="tab">TabPage容器控件</param>
        /// <returns>新建的Text Box控件</returns>
        private FastColoredTextBox CreateTextBoxInTab(TabPage tab)
        {
            FastColoredTextBox textBox = new FastColoredTextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("Microsoft YaHei", 11F);
            tab.Controls.Add(textBox);
            return textBox;
        }

        /// <summary>
        /// 设置Json文本高亮格式
        /// </summary>
        private void JsonTextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(mBrownStyle, mMagentaStyle, mGreenStyle);
            //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers("{", "}");
            //string highlighting
            e.ChangedRange.SetStyle(mBrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //number highlighting
            e.ChangedRange.SetStyle(mGreenStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        /// <summary>
        /// 使用BackgroundWorker加载Excel文件，使用UI中的Options设置
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        private void LoadExcelAsync(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            //-- update ui
            this.btnReimport.Enabled = true;
            EnableExportButtons(false);

            this.statusLabel.IsLink = false;
            this.statusLabel.Text = "正在读取Excel ...";

            //-- start import
            this.backgroundWorker.RunWorkerAsync(path);
        }

        /// <summary>
        /// 接受Excel拖放事件
        /// </summary>
        private void lvwData_DragDrop(object sender, DragEventArgs e)
        {
            StringBuilder filesName = new StringBuilder("");
            Array file = (System.Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储

            foreach (object I in file)
            {
                string path = I.ToString();

                System.IO.FileInfo info = new System.IO.FileInfo(path);
                //若为目录，则获取目录下所有子文件名
                if ((info.Attributes & System.IO.FileAttributes.Directory) != 0)
                {
                    LoadFolder(path);
                }
                //若为文件，则获取文件名
                else if (System.IO.File.Exists(path) && JudgeExcel(path))
                {
                    AddExcel(path);
                }
            }

            lvwData.Refresh();
        }

        /// <summary>
        /// 判断拖放对象是否是一个.xlsx或者xls文件
        /// </summary>
        private void lvwData_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件
            else e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 执行实际的Excel加载
        /// </summary>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (mDataMgr)
            {
                mDataMgr.loadExcel((string)e.Argument);
            }
        }

        /// <summary>
        /// Excel加载完成
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            // 判断错误信息
            if (e.Error != null)
            {
                ShowStatus(e.Error.Message, Color.Red);
                return;
            }

            // 更新UI
            lock (this.mDataMgr)
            {
                this.statusLabel.IsLink = false;
                this.statusLabel.Text = "Load completed.";

                mJSONTextBox.Text = mDataMgr.JsonContext;
                mCSharpTextBox.Text = mDataMgr.CSharpCode;

                EnableExportButtons(true);
            }
        }

        /// <summary>
        /// 工具栏按钮：Import Excel
        /// </summary>
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();//提示用户打开文件窗体
            dlg.Description = "请选择文件夹路径";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadFolder(dlg.SelectedPath);
            }
        }

        /// <summary>
        /// 保存导出文件
        /// </summary>
        private void SaveToFile(int type, string filter)
        {
            string FileName = GetNowFileName();
            if (string.IsNullOrEmpty(FileName))
            {
                ShowStatus("请选择一项！", Color.Black);
                return;
            }
            try
            {
                var path = mDataMgr.options.OutputPath;
                string outputPath;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    outputPath = path;
                    switch (type)
                    {
                        case 1:
                            outputPath = $"{outputPath}/JSON/{FileName}.json";
                            break;
                        case 2:
                            outputPath = $"{outputPath}/CS/{FileName}.cs";
                            break;
                    }
                }
                else
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.RestoreDirectory = true;
                    dlg.Filter = filter;
                    dlg.FileName = FileName;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        outputPath = dlg.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                lock (mDataMgr)
                {
                    switch (type)
                    {
                        case 1:
                            mDataMgr.saveJson(outputPath);
                            break;
                        case 2:
                            mDataMgr.saveCSharp(outputPath);
                            break;
                    }
                }
                ShowStatus($"{FileName} saved!", Color.Black);
            }
            catch (Exception ex)
            {
                ShowStatus(ex.Message, Color.Red);
            }
        }

        /// <summary>
        /// 设置状态栏信息
        /// </summary>
        /// <param name="szMessage">信息文字</param>
        /// <param name="color">信息颜色</param>
        private void ShowStatus(string szMessage, Color color)
        {
            this.statusLabel.Text = szMessage;
            this.statusLabel.ForeColor = color;
            this.statusLabel.IsLink = false;
        }

        /// <summary>
        /// 配置项变更之后，手动重新导入xlsx文件
        /// </summary>
        private void btnReimport_Click(object sender, EventArgs e)
        {
            lvwData.Items.Clear();
            LoadFolder(mDataMgr.options.InPath);
        }
        private void btnSaveJSON_Click(object sender, EventArgs e)
        {
            SaveToFile(1, "Json File(*.json)|*.json");
        }
        private void btnCopyJSON_Click(object sender, EventArgs e)
        {
            lock (mDataMgr)
            {
                Clipboard.SetText(mDataMgr.JsonContext);
                ShowStatus("Json text copyed to clipboard.", Color.Black);
            }
        }
        private void btnCopyCSharp_Click(object sender, EventArgs e)
        {
            lock (mDataMgr)
            {
                Clipboard.SetText(mDataMgr.CSharpCode);
                ShowStatus("C# code copyed to clipboard.", Color.Black);
            }
        }
        private void btnSaveCSharp_Click(object sender, EventArgs e)
        {
            SaveToFile(2, "C# code file(*.cs)|*.cs");
        }
        private void btnCopyXML_Click(object sender, EventArgs e)
        {

        }
        private void btnSaveXML_Click(object sender, EventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void btnConfig_Click(object sender, EventArgs e)
        {
            var config = new Config(mDataMgr.options);
            if (config.ShowDialog() == DialogResult.OK)
            {
                Options.SaveINI(mDataMgr.options);
                ShowStatus("保存成功", Color.Black);
            }
        }

        /// <summary>
        /// 将文件添加到列表中
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="path">文件路径</param>
        private ListViewItem AddExcel(string name, string path)
        {
            if (IsExcelInListView(path))
            {
                return null;
            }
            var item = lvwData.Items.Add(name);
            item.SubItems.Add(path);
            return item;
        }

        private ListViewItem AddExcel(string path)
        {
            var FileName = Path.GetFileNameWithoutExtension(path);
            return AddExcel(FileName, path);
        }

        /// <summary>
        /// 判断路径扩展名是不是excel的
        /// </summary>
        /// <param name="path"></param>
        private bool JudgeExcel(string path)
        {
            string szExt = System.IO.Path.GetExtension(path);
            szExt = szExt.ToLower();
            if (szExt == ".xlsx" || szExt == ".xls")
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 获取当前的文件名
        /// </summary>
        /// <returns></returns>
        private string GetNowFileName()
        {
            if (lvwData.SelectedItems == null)
            {
                return "";
            }
            return lvwData.SelectedItems[0].Text;
        }

        private bool IsExcelInListView(string path)
        {
            for (int i = 0; i < lvwData.Items.Count; i++)
            {
                if (lvwData.Items[i].SubItems[1].Text.Equals(path))
                {
                    return true;
                }
            }
            return false;
        }
        private void lvwData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwData.SelectedItems.Count == 0)
            {
                mJSONTextBox.Clear();
                mXMLTextBox.Clear();
                mCSharpTextBox.Clear();
                EnableExportButtons(false);
                return;
            }
            var item = lvwData.SelectedItems[0];
            if (item != null)
            {
                LoadExcelAsync(item.SubItems[1].Text);
            }
        }

        private void LoadFolder(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;
            //遍历子文件夹导入所有excel
            DirectoryInfo d = new DirectoryInfo(path);
            FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                if (fsinfo is DirectoryInfo)     //判断是否为文件夹
                {
                    LoadFolder(fsinfo.FullName);//递归调用
                }
                else
                {
                    if (JudgeExcel(fsinfo.FullName))
                    {
                        AddExcel(fsinfo.FullName);
                    }
                }
            }
        }

    }
}
