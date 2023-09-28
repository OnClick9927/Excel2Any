using FastColoredTextBoxNS;
using System;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public partial class TextConvertPage : BaseConvertPage
    {
        protected mTextBox txtCode = new mTextBox();
        public TextConvertPage()
        {
            InitializeComponent();
            txtCode.Dock = DockStyle.Fill;
            txtCode.Visible = false;
            txtCode.TextChanged += txtCode_TextChanged;
        }


        public override void tabSheets_SelectedIndexChanged(object sender, EventArgs e)
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
        public override void RefreshSheet()
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


        protected virtual void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
