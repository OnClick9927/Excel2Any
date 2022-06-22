using Microsoft.WindowsAPICodePack.Dialogs;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public partial class BaseConvertPage : UIPage
    {
        List<SheetContent> _sheets;
        public Action<string> onFolderOpen;
        public Action onFolderRefresh;
        public Action onSettingClick;
        public Action onSave;

        public BaseConvertPage()
        {
            InitializeComponent();
            txtCode.Dock = DockStyle.Fill;
            txtCode.Visible = false;
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="sheets"></param>
        public void SetSheets(List<SheetContent> sheets)
        {
            _sheets = sheets;
        }
        /// <summary>
        /// 将文件列表控件移过来
        /// </summary>
        /// <param name="tvwFile"></param>
        public void SetFileListControl(Control tvwFile)
        {
            pnlFiles.Controls.Add(tvwFile);
        }

        private void tabSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSheets.SelectedIndex == -1) return;
            tabSheets.TabPages[tabSheets.SelectedIndex].Controls.Add(txtCode);

            //这里为了防止在清空节点重新添加途中报错所以无脑判断了一下是否越界
            if (tabSheets.SelectedIndex <= _sheets.Count - 1)
            {
                txtCode.Text = _sheets[tabSheets.SelectedIndex].content;
            }
        }

        /// <summary>
        /// 刷新Sheet
        /// </summary>
        public void RefreshSheet()
        {
            //清除所有Sheet
            tabSheets.TabPages.Clear();
            if (_sheets == null || _sheets.Count == 0)
            {
                txtCode.Visible = false;
                return;
            }
            for (int i = 0; i < _sheets.Count; i++)
            {
                var tabPage = new TabPage(_sheets[i].sheetName);
                tabPage.BackColor = Color.FromArgb(255, 30, 30, 30);
                tabSheets.TabPages.Add(tabPage);
            }

            //将文本放到第一个sheet
            txtCode.Visible = true;
            tabSheets.TabPages[0].Controls.Add(txtCode);
            txtCode.Text = _sheets[0].content;
            tabSheets.SelectTab(0);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                UIMessageTip.ShowOk(dialog.FileName);
                onFolderOpen?.Invoke(dialog.FileName);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabSheets.TabPages.Clear();
            txtCode.Text = "";
            onFolderRefresh?.Invoke();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            onSettingClick?.Invoke();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            onSave?.Invoke();
        }

        private void MenuItemCopyAll_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCode.Text);
            UIMessageTip.ShowOk("已将所有文本复制到剪贴板");
        }

        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            txtCode.Copy();
            UIMessageTip.ShowOk("已将选中文本复制到剪贴板");
        }



        #region 文本框匹配




        #endregion

    }
}
