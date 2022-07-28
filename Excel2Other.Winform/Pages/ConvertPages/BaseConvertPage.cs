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
        public List<SheetData> _sheets;
        public Action<string> onFolderOpen;
        public Action onFolderRefresh;
        public Action onSettingClick;
        public Action onSave;

        public Type entityType;


        public BaseConvertPage()
        {
            InitializeComponent();
        }

        public void SetEntityType(Type entityType)
        {
            this.entityType = entityType;
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="sheets"></param>
        public void SetSheets(List<SheetData> sheets)
        {
            _sheets = sheets;
            RefreshSheet();
        }
        /// <summary>
        /// 将文件列表控件移过来
        /// </summary>
        /// <param name="tvwFile"></param>
        public void SetFileListControl(Control tvwFile)
        {
            pnlFiles.Controls.Add(tvwFile);
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
        private void btnSetting_Click(object sender, EventArgs e)
        {
            onSettingClick?.Invoke();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            onSave?.Invoke();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabSheets.TabPages.Clear();
            onFolderRefresh?.Invoke();
        }

        public virtual void RefreshSheet() { }
        /// <summary>
        /// 刷新Sheet
        /// </summary>
        /// <returns>如果没有sheet返回false </returns>
        protected bool RefreshTab()
        {
            //清除所有Sheet
            tabSheets.TabPages.Clear();
            if (_sheets == null || _sheets.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < _sheets.Count; i++)
            {
                var tabPage = new TabPage(_sheets[i].sheetName);
                tabPage.BackColor = Color.FromArgb(255, 30, 30, 30);
                tabSheets.TabPages.Add(tabPage);
            }
            return true;
        }
    }
}
