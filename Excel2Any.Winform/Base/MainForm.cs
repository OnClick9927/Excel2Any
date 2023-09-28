using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Sunny.UI;

namespace Excel2Any.Winform
{
    public partial class MainForm : UIAsideMainFrame
    {
        private FormSetting _formSetting = null;    //窗体配置

        const int settingPageIndex = 9999;
        public MainForm()
        {
            _formSetting = SettingHelper.formSetting;
            var commonSetting = SettingHelper.GetSettingBySettingType(typeof(CommonSetting)) as CommonSetting;
            ExcelHelper.Init(commonSetting); //初始化ExcelHelper
            InitializeComponent();
            tvwFile.Dock = DockStyle.Fill;

            //添加设置页面
            var settingPage = new SettingPage();
            settingPage.RefreshMainFormPlanList += RefeshPlanList;
            AddPage(settingPage, settingPageIndex);


            var fields = SettingHelper.GetFields(typeof(FormSetting));
            settingPage.CreateSettingTab(_formSetting, "窗体");
            settingPage.CreateSettingTab(commonSetting, "通用");

            foreach (var entityType in ExcelHelper.GetAllEntityTypes())
            {
                var uiEntity = UIEntityHelper.GetUIEntity(entityType);
                SettingHelper.LoadSetting(entityType);
                AddPage(uiEntity.page, uiEntity.pageIndex);
                var jsonNode = Aside.CreateNode(uiEntity.name, uiEntity.symbol, 30, uiEntity.pageIndex);
                jsonNode.ToolTipText = uiEntity.name;

                uiEntity.page.onFolderOpen += OpenFileOrDirectory;
                uiEntity.page.onFolderRefresh += ReloadPaths;

                uiEntity.page.onSettingClick += () =>
                {
                    Aside.SelectedNode = null;
                    SelectPage(settingPageIndex);
                    settingPage.OpenSetting(uiEntity.name);
                };

                uiEntity.page.onSave += () => SaveAllFiles(entityType);

                settingPage.CreateSettingTab(uiEntity.setting, uiEntity.name, entityType);
            }

            Aside.SelectFirst();

            //程序设置部分
            var path = _formSetting.lastOpenPath;
            if (_formSetting.openLast && !string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                OpenFileOrDirectory(path);
            }

            AsideWidthChange(_formSetting.isExpand);

            //添加menuContext
            RefeshPlanList();

            ctxMenu.ItemClicked += (o, e) =>
            {
                SettingHelper.SetPlan(e.ClickedItem.Text);
                settingPage.RefreshUI();
                RefreshPlanLabel();
                //打勾
                foreach (ToolStripMenuItem item in ctxMenu.Items)
                {
                    item.Checked = false;
                }
                ((ToolStripMenuItem)e.ClickedItem).Checked = true;

                lblPlanName.Text = e.ClickedItem.Text;
            };

        }

        public void RefeshPlanList()
        {
            ctxMenu.Items.Clear();
            foreach (var item in SettingHelper.GetPlanList())
            {
                var newItem = ctxMenu.Items.Add(item);
                if (_formSetting.plan.Equals(item))
                {
                    ((ToolStripMenuItem)newItem).Checked = true;
                    lblPlanName.Text = item;
                }
            }
            RefreshPlanLabel();
        }

        BaseConvertPage currentPage = null;
        private void Aside_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            if (pageIndex != -1 && item.Text != "设置")
            {
                ((BaseConvertPage)GetPage(pageIndex)).SetFileListControl(tvwFile);
                currentPage = (BaseConvertPage)GetPage(pageIndex);
                GetAndSetSheets(currentPage, tvwFile.SelectedNode);
            }

        }
        private void AsideWidthChange(bool expand)
        {
            Aside.Width = expand ? 120 : 55;
            Aside.ShowNodeToolTips = !expand;
        }
        private void tvwFile_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            GetAndSetSheets(currentPage, e.Node);
            //这一句是为了防止TreeView点击之后图标改变
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }

        public void GetAndSetSheets(BaseConvertPage page, TreeNode node)
        {
            if (page != null && node != null)
            {
                var path = tvwFile.Nodes[0].Tag + node.FullPath;
                if (IsExcelFile(path))
                {
                    page.SetSheetsAndRows(ExcelHelper.GetSheets(currentPage.entityType, path), ExcelHelper.GetRowHeads(currentPage.entityType, path),path);
                }

            }
        }

        private void tvwFile_DragEnter(object sender, DragEventArgs e)
        {
            //if (tvwFile.Nodes.Count > 0 && Directory.Exists(tvwFile.Nodes[0].Tag + tvwFile.Nodes[0].Text))
            //{
            //    e.Effect = DragDropEffects.None;
            //    return;
            //}
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
                    UIMessageTip.ShowError("请拖入文件夹！");
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

            _formSetting.lastOpenPath = path;
            SettingHelper.SaveSetting(_formSetting);
            ((SettingPage)GetPage(settingPageIndex))?.RefreshUI();
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
                        (_formSetting.excludeFile
                            && !string.IsNullOrWhiteSpace(_formSetting.excludeFileString)
                            && file.Contains(_formSetting.excludeFileString))) continue;
                    //只添加excel文件
                    if (IsExcelFile(file))
                    {
                        var fileName = file.Substring(file.LastIndexOf('\\') + 1);

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

        #region 保存文件
        /// <summary>
        /// 一键保存
        /// </summary>
        /// <param name="type">保存类型</param>
        private void SaveAllFiles(Type type)
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
                    ExcelHelper.Save(type, FormatPath(tvwFile.Nodes[i].Tag + tvwFile.Nodes[i].Text));
                }
            }
            else
            {
                //文件夹的话则遍历到所有叶子节点并保存
                //直接递归判断imageIndex 是不是1 ，是1就是叶子节点23333
                SaveNode(tvwFile.Nodes[0], type);
            }

        }
        private void SaveNode(TreeNode node, Type entity)
        {
            if (node.ImageIndex == 1)
            {
                ExcelHelper.Save(entity, FormatPath(tvwFile.Nodes[0].Tag + node.FullPath));
                return;
            }

            if (node.Nodes.Count == 0) return;
            foreach (TreeNode tnSub in node.Nodes)
            {
                SaveNode(tnSub, entity);
            }
        }
        #endregion

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Aside.SelectedNode = null;
            SelectPage(settingPageIndex);
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            //ShowInfoDialog(btnPlanName.Location.ToString());
            btnSetting.Location = new Point(btnSetting.Location.X, this.Size.Height - btnSetting.Height);

            RefreshPlanLabel();
        }

        private void RefreshPlanLabel()
        {
            lblPlanName.Location = new Point(this.Width - lblPlanName.Width - 125, 0);
        }
    }

}