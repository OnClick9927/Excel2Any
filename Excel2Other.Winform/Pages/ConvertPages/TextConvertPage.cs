using Microsoft.WindowsAPICodePack.Dialogs;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public partial class TextConvertPage : BaseConvertPage
    {
        public TextConvertPage()
        {
            InitializeComponent();
            txtCode.Dock = DockStyle.Fill;
            txtCode.Visible = false;
        }

        private void tabSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSheets.SelectedIndex == -1) return;
            tabSheets.TabPages[tabSheets.SelectedIndex].Controls.Add(txtCode);

            //这里为了防止在清空节点重新添加途中报错所以无脑判断了一下是否越界
            if (tabSheets.SelectedIndex <= _sheets.Count - 1)
            {
                txtCode.Text = _sheets[tabSheets.SelectedIndex].content.ToString();
            }
        }

        /// <summary>
        /// 刷新Sheet
        /// </summary>
        public void RefreshSheet()
        {
            txtCode.Visible = false;

            if (RefreshTab())
            {
                //将文本放到第一个sheet
                txtCode.Visible = true;
                tabSheets.TabPages[0].Controls.Add(txtCode);
                txtCode.Text = _sheets[0].content.ToString();
                tabSheets.SelectTab(0);
            }


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


    }
}
