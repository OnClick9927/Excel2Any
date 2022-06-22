using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunny.UI;

namespace Excel2Other.Winform
{
    public partial class MainForm : UIAsideMainFrame
    {
        private ExcelReader reader;
        private JsonSettings jsonSetting;
        private CSharpSettings cSharpSetting;
        private XmlSettings xmlSetting;
        private FormSetting formSetting; //窗体配置
        private Action onOpenFile; //打开文件

        JsonConvertPage jsonPage;
        XMLConvertPage xmlPage;
        CSharpConvertPage csharpPage;

        public MainForm()
        {
            InitializeComponent();

            //读取本地设置
            LoadSettings();
            reader = new ExcelReader();
            reader.SetSetting(jsonSetting);
            reader.SetSetting(cSharpSetting);
            reader.SetSetting(xmlSetting);

            int pageIndex = 1000;

            //添加Json页面
            jsonPage = new JsonConvertPage();
            AddPage(jsonPage, ++pageIndex);
            var jsonNode = Aside.CreateNode("Json", 261787, 30, pageIndex);
            //添加Xml页面
            xmlPage = new XMLConvertPage();
            AddPage(xmlPage, ++pageIndex);
            var xmlNode = Aside.CreateNode("Xml", 261891, 30, pageIndex);
            //添加CSharp页面
            csharpPage = new CSharpConvertPage();
            AddPage(csharpPage, ++pageIndex);
            var csNode = Aside.CreateNode("C#", 261897, 30, pageIndex);
            //添加设置页面
            var settingPage = new SettingPage(jsonSetting, cSharpSetting, xmlSetting, formSetting);
            AddPage(settingPage, ++pageIndex);
            //Aside.CreateNode("设置", 61459, 30, pageIndex);

            tvwFile.Dock = DockStyle.Fill;

            //选择第一个界面
            Aside.SelectPage(1001);

            #region 子界面的按钮设置
            jsonPage.onFolderOpen += OpenFileOrDirectory;
            csharpPage.onFolderOpen += OpenFileOrDirectory;
            xmlPage.onFolderOpen += OpenFileOrDirectory;

            jsonPage.onFolderRefresh += ReloadPaths;
            csharpPage.onFolderRefresh += ReloadPaths;
            xmlPage.onFolderRefresh += ReloadPaths;

            jsonPage.onSettingClick += () =>
            {
                Aside.SelectPage(1004);
                settingPage.OpenSetting("Json设置");
            };
            csharpPage.onSettingClick += () =>
            {
                Aside.SelectPage(1004);
                settingPage.OpenSetting("C#设置");
            };
            xmlPage.onSettingClick += () =>
            {
                Aside.SelectPage(1004);
                settingPage.OpenSetting("Xml设置");
            };

            jsonPage.onSave += () => SaveAllFiles(ConvertType.Json); ;
            xmlPage.onSave += () => SaveAllFiles(ConvertType.Xml); ;
            csharpPage.onSave += () => SaveAllFiles(ConvertType.CSharp); ;
            #endregion 

            onOpenFile += jsonPage.RefreshSheet;
            onOpenFile += csharpPage.RefreshSheet;
            onOpenFile += xmlPage.RefreshSheet;

            //设置部分的委托
            settingPage.onJsonSave += (selectPath) => SaveSetting(jsonSetting, "jconfig", selectPath ? "" : "Settings/JsonSettings.jconfig");
            settingPage.onCSharpSave += (selectPath) => SaveSetting(cSharpSetting, "csconfig", selectPath ? "" : "Settings/CSharpSettings.csconfig");
            settingPage.onXmlSave += (selectPath) => SaveSetting(xmlSetting, "xconfig", selectPath ? "" : "Settings/XmlSettings.xconfig");
            settingPage.onFormSave += SaveFormSetting;

            settingPage.onJsonLoad += () => { LoadJsonSetting(); settingPage.InitJsonSettings(); };
            settingPage.onCSharpLoad += () => { LoadCSharpSetting(); settingPage.InitCSharpSettings(); };
            settingPage.onXmlLoad += () => { LoadXmlSetting(); settingPage.InitXmlSettings(); };



            //程序设置部分
            if (formSetting.openLast && !string.IsNullOrEmpty(formSetting.lastOpenPath) && Directory.Exists(formSetting.lastOpenPath))
            {
                OpenFileOrDirectory(formSetting.lastOpenPath);
            }
        }

        private void Aside_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            if (pageIndex != -1 && item.Text != "设置")
            {
                ((BaseConvertPage)GetPage(pageIndex)).SetFileListControl(tvwFile);
            }
        }
        private void tvwFile_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var path = tvwFile.Nodes[0].Tag + e.Node.FullPath;
            //判断是否存在excel文件，如果存在则打开
            if (IsExcelFile(path))
            {
                //UIMessageTip.ShowOk(e.Node.FullPath);
                reader.Read(path);
                jsonPage.SetSheets(reader.GetSheets(ConvertType.Json));
                xmlPage.SetSheets(reader.GetSheets(ConvertType.Xml));
                csharpPage.SetSheets(reader.GetSheets(ConvertType.CSharp));
                onOpenFile?.Invoke();
            }

            //这一句是为了防止TreeView点击之后图标改变
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }
        private void tvwFile_DragEnter(object sender, DragEventArgs e)
        {
            if (tvwFile.Nodes.Count > 0 && Directory.Exists(tvwFile.Nodes[0].Tag + tvwFile.Nodes[0].Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            e.Effect = DragDropEffects.All;
        }
        private void tvwFile_DragDrop(object sender, DragEventArgs e)
        {

            string[] dropData = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (dropData == null)
            {
                UIMessageTip.ShowError("你拖入了什么玩意？？？？？");
                return;
            }
            //如果拖入的文件是多个，暂时不做处理直接报错提示
            if (dropData.Length >= 2)
            {
                UIMessageTip.ShowError("请勿移入多个文件");
                return;
            }

            OpenFileOrDirectory(dropData[0]);

        }

        #region 文件列表相关代码
        /// <summary>
        /// 路径类型
        /// </summary>
        enum PathType
        {
            /// <summary>
            /// 文件
            /// </summary>
            File,
            /// <summary>
            /// 文件夹
            /// </summary>
            Folder,
            /// <summary>
            /// 路径不存在
            /// </summary>
            NotExist
        }

        /// <summary>
        /// 打开文件夹(当TreeView中没有文件夹时||只有文件时||重新打开文件夹)
        /// </summary>
        private void OpenFileOrDirectory(string path)
        {
            //判断拖入的是文件还是文件夹
            PathType pathType = GetPathType(path);
            switch (pathType)
            {
                case PathType.File:
                    OpenFile(path);
                    break;
                case PathType.Folder:
                    OpenDirectory(path);
                    break;
                default:
                    UIMessageTip.ShowError("文件路径不存在");
                    return;
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="path">路径</param>
        private void OpenFile(string path)
        {
            //如果是文件则单独将文件加入TreeView
            if (IsExcelFile(path))
            {
                if (IsInNodes(path))
                {
                    UIMessageTip.ShowError("文件已经在列表中了");
                }
                else
                {
                    UIMessageTip.ShowOk("这是一个Excel文件");
                    var node = tvwFile.Nodes.Add(path.Substring(path.LastIndexOf('\\') + 1));
                    //将路径存到tag中 增加TreeView的可读性
                    node.Tag = path.Substring(0, path.LastIndexOf('\\') + 1);
                    node.ToolTipText = path;
                    node.ImageIndex = 1;
                }
            }
            else
            {
                UIMessageTip.ShowError("这不是Excel文件");
            }
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="path">路径</param>
        private void OpenDirectory(string path)
        {
            //UIMessageTip.ShowOk("这是一个文件夹");
            tvwFile.Nodes.Clear();
            path = FormatPath(path);

            //将文件夹作为根目录
            var rootNode = tvwFile.Nodes.Add(path.Substring(path.LastIndexOf('\\') + 1));
            rootNode.Tag = path.Substring(0, path.LastIndexOf('\\') + 1);
            rootNode.ToolTipText = path;
            rootNode.ImageIndex = 0;
            //遍历文件夹加入节点
            LoadDirectoy(path, rootNode);

            tvwFile.ExpandAll();


            //更新设置
            formSetting.lastOpenPath = path;
            SaveFormSetting();
        }

        /// <summary>
        /// 递归遍历文件夹及其文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="node">根节点</param>
        private void LoadDirectoy(string path, TreeNode node)
        {
            //获取所有的子文件夹
            var directories = Directory.GetDirectories(path);
            if (directories != null)
            {
                foreach (var item in directories)
                {
                    //路径去掉前面的
                    var directoryNode = node.Nodes.Add(item.Substring(item.LastIndexOf('\\') + 1));
                    directoryNode.ImageIndex = 0;
                    //递归遍历文件夹
                    LoadDirectoy(item, directoryNode);
                }
            }

            //遍历文件
            var files = Directory.GetFiles(path);
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (Path.GetFileName(file).StartsWith("~$") ||
                        (formSetting.excludeFile
                            && !string.IsNullOrWhiteSpace(formSetting.excludePrefix)
                            && file.StartsWith(formSetting.excludePrefix))) continue;
                    //只添加excel文件
                    if (IsExcelFile(file))
                    {
                        var fileName = file.Substring(file.LastIndexOf('\\') + 1);

                        //排除文件头设置
                        if (formSetting.excludeFile
                            && !string.IsNullOrWhiteSpace(formSetting.excludePrefix)
                            && fileName.StartsWith(formSetting.excludePrefix))
                        {
                            continue;
                        }

                        var fileNode = node.Nodes.Add(fileName);
                        //fileNode.ToolTipText = file;
                        fileNode.ImageIndex = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 判断文件或者文件夹是否存在并确定类型
        /// </summary>
        /// <returns>返回枚举文件夹或者文件</returns>
        private PathType GetPathType(string path)
        {
            if (File.Exists(path))
            {
                return PathType.File;
            }
            else if (Directory.Exists(path))
            {
                return PathType.Folder;
            }
            else
            {
                return PathType.NotExist;
            }
        }

        /// <summary>
        /// 根据扩展名判断路径是否是excel文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>如果是excel文件则返回是</returns>
        private bool IsExcelFile(string path)
        {
            if (File.Exists(path))
            {
                var extension = Path.GetExtension(path).ToLower();
                return extension.Equals(".xls") || extension.Equals(".xlsx");
            }
            return false;
        }

        /// <summary>
        /// 判断文件是否已经在节点中了
        /// </summary>
        /// <returns></returns>
        private bool IsInNodes(string path)
        {
            for (int i = 0; i < tvwFile.Nodes.Count; i++)
            {
                if ((tvwFile.Nodes[i].Tag + tvwFile.Nodes[i].Text).Equals(FormatPath(path)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 格式化路径字符串
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>格式化后的路径</returns>
        private string FormatPath(string path)
        {
            path = path.Replace("/", "\\").Replace("\\\\", "\\");
            if (path.EndsWith("\\"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            return path;
        }

        /// <summary>
        /// 重新读取当前路径
        /// </summary>
        private void ReloadPaths()
        {
            //如果没有节点
            if (tvwFile.Nodes.Count == 0) return;

            //根节点是文件的情况下判断文件是否存在，如果不存在就删除节点
            if (IsExcelFile(tvwFile.Nodes[0].Tag + tvwFile.Nodes[0].Text))
            {
                for (int i = tvwFile.Nodes.Count - 1; i >= 0; i--)
                {
                    if (!File.Exists(tvwFile.Nodes[i].Tag + tvwFile.Nodes[i].Text))
                    {
                        tvwFile.Nodes.RemoveAt(i);
                    }
                }
            }
            else
            {
                //根节点是文件夹 重新打开
                OpenFileOrDirectory(tvwFile.Nodes[0].Tag + tvwFile.Nodes[0].Text);
            }
        }

        #endregion

        #region 设置相关

        //说明：
        //配置文件根据不同的配置保存为不同的文件（Json配置一个文件，Csharp配置一个文件……）
        //配置位置在程序目录的Settings文件夹下
        //设置写得乱七八糟


        /// <summary>
        /// 读取本地设置
        /// </summary>
        private void LoadSettings()
        {
            if (!Directory.Exists("Settings"))
            {
                Directory.CreateDirectory("Settings");
            }
            //读取本地设置
            jsonSetting = GetConvertSetting<JsonSettings>("Settings/JsonSettings.jconfig", true);
            if (jsonSetting == null)
            {
                jsonSetting = new JsonSettings();
                SaveJsonSetting();
            }

            xmlSetting = GetConvertSetting<XmlSettings>("Settings/XmlSettings.xconfig", true);
            if (xmlSetting == null)
            {
                xmlSetting = new XmlSettings();
                SaveXmlSetting();
            }


            cSharpSetting = GetConvertSetting<CSharpSettings>("Settings/CSharpSettings.csconfig", true);
            if (cSharpSetting == null)
            {
                cSharpSetting = new CSharpSettings();
                SaveCSharpSetting();
            }

            formSetting = GetConvertSetting<FormSetting>("Settings/FormSetting.mainconfig", true);
            if (formSetting == null)
            {
                formSetting = new FormSetting();
                SaveFormSetting();
            }

        }

        /// <summary>
        /// 返回文件读取的字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ReadJsonText(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    return file.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        private T GetConvertSetting<T>(string path, bool init = false)
        {
            var text = ReadJsonText(path);
            if (string.IsNullOrWhiteSpace(text))
            {
                if (!init)
                    ShowErrorTip($"配置文件不存在或为空白！");
                return default;
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(text);
                }
                catch (Exception)
                {
                    if (!init)
                        ShowErrorDialog("配置文件有误");
                    return default;
                }
            }
        }

        private void SaveSetting<T>(T settings, string extention, string path = "")
        {
            bool isSelectSave = false;
            if (settings == null) return;

            if (string.IsNullOrEmpty(path))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = $"配置文件|*.{extention}";     //设置保存类型
                save.Title = "请设置Json配置的保存位置和文件名";   //对话框标题
                if (save.ShowDialog() == DialogResult.OK)
                {
                    path = save.FileName;
                    isSelectSave = true;
                }
                else
                {
                    return;
                }
            }

            try
            {
                var text = JsonConvert.SerializeObject(settings, Formatting.Indented);

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    //写入
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(text);
                    }
                    if (isSelectSave)
                    {
                        ShowSuccessDialog("保存完毕");
                    }
                }
            }
            catch (Exception e)
            {

                UIMessageDialog.ShowErrorDialog(this, e.Message);
            }
        }

        private void SaveJsonSetting()
        {
            SaveSetting(jsonSetting, "jconfig", "Settings/JsonSettings.jconfig");
        }

        private void SaveCSharpSetting()
        {
            SaveSetting(cSharpSetting, "csconfig", "Settings/CSharpSettings.csconfig");
        }

        private void SaveXmlSetting()
        {
            SaveSetting(xmlSetting, "xconfig", "Settings/XmlSettings.xconfig");
        }

        private void SaveFormSetting()
        {
            SaveSetting(formSetting, "mainconfig", "Settings/FormSetting.mainconfig");
        }

        private void LoadJsonSetting()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择Json配置";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = "配置文件|*.jconfig";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var setting = GetConvertSetting<JsonSettings>(dialog.FileName);
                if (setting != null)
                {
                    jsonSetting = setting;
                    SaveJsonSetting();
                    ShowSuccessTip("读取完毕");
                }
            }
        }

        private void LoadXmlSetting()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择Xml配置";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = "配置文件|*.xconfig";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var setting = GetConvertSetting<XmlSettings>(dialog.FileName);
                if (setting != null)
                {
                    xmlSetting = setting;
                    SaveXmlSetting();
                    ShowSuccessTip("读取完毕");
                }
            }
        }

        private void LoadCSharpSetting()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择C#配置";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = "配置文件|*.csconfig";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var setting = GetConvertSetting<CSharpSettings>(dialog.FileName);
                if (setting != null)
                {
                    cSharpSetting = setting;
                    SaveCSharpSetting();
                    ShowSuccessTip("读取完毕");
                }
            }
        }

        #endregion

        #region 保存文件
        /// <summary>
        /// 一键保存
        /// </summary>
        /// <param name="type">保存类型</param>
        private void SaveAllFiles(ConvertType type)
        {
            //保存前自动刷新目录
            ReloadPaths();
            if (tvwFile.Nodes.Count == 0) return;

            //判断根节点是文件还是文件夹
            //如果是文件则遍历节点保存
            if (File.Exists(tvwFile.Nodes[0].Tag + tvwFile.Nodes[0].Text))
            {
                for (int i = 0; i < tvwFile.Nodes.Count; i++)
                {
                    reader.Read(FormatPath(tvwFile.Nodes[i].Tag + tvwFile.Nodes[i].Text));
                    reader.Save(type);
                }
            }
            else
            {
                //文件夹的话则遍历到所有叶子节点并保存
                //直接递归判断imageIndex 是不是1 ，是1就是叶子节点23333
                LoadNode(tvwFile.Nodes[0], type);
            }

        }

        private void LoadNode(TreeNode node, ConvertType type)
        {
            if (node.ImageIndex == 1)
            {
                reader.Read(FormatPath(tvwFile.Nodes[0].Tag + node.FullPath));
                reader.Save(type);
                return;
            }

            if (node.Nodes.Count == 0) return;
            foreach (TreeNode tnSub in node.Nodes)
            {
                LoadNode(tnSub, type);
            }
        }
        #endregion

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Aside.SelectedNode = null;
            SelectPage(1004);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            btnSetting.Location = new Point(btnSetting.Location.X, this.Size.Height - btnSetting.Height);
        }
    }

}