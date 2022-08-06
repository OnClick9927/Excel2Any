using FastColoredTextBoxNS;
using Sunny.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public partial class mTextBox : Panel
    {

        UIScrollBar VBar = new UIScrollBar();
        FastColoredTextBox textBox = new FastColoredTextBox();

        UIContextMenuStrip menuStrip = new UIContextMenuStrip();
        ToolStripMenuItem menuItemCopy = new ToolStripMenuItem();
        ToolStripMenuItem menuItemCopyAll = new ToolStripMenuItem();

        ComponentResourceManager resources = new ComponentResourceManager(typeof(TextConvertPage));



        public string text  => textBox.Text;


        public mTextBox()
        {
            InitializeComponent();
            textBox.Dock = DockStyle.Fill;
            VBar.Parent = this;
            VBar.StyleCustomMode = true;
            VBar.FillColor = Color.FromArgb(30, 30, 30);
            VBar.ForeColor = Color.FromArgb(66, 66, 66);
            VBar.HoverColor = Color.FromArgb(79, 79, 79);
            VBar.PressColor = Color.FromArgb(94, 94, 94);
            VBar.ShowLeftLine = false;

            textBox.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '\"', '\"', '\'', '\'' };
            textBox.AutoScrollMinSize = new Size(0, 0);
            textBox.BackBrush = null;
            textBox.BackColor = Color.FromArgb(30, 30, 30);
            textBox.CharCnWidth = 17;
            textBox.CharHeight = 14;
            textBox.CharWidth = 8;
            textBox.ContextMenuStrip = menuStrip;
            textBox.Cursor = Cursors.IBeam;
            textBox.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            textBox.ForeColor = Color.FromArgb(212, 212, 212);
            textBox.ImeMode = ImeMode.Off;
            textBox.IndentBackColor = Color.FromArgb(30, 30, 30);
            textBox.IsReplaceMode = false;
            textBox.LineNumberColor = Color.FromArgb(133, 133, 133);
            textBox.Location = new Point(50, 50);
            textBox.Paddings = new Padding(0);
            textBox.SelectionColor = Color.FromArgb(60, 0, 12, 215);
            textBox.ServiceColors = (ServiceColors)(resources.GetObject("txtCode.ServiceColors"));
            textBox.ServiceLinesColor = Color.FromArgb(64, 64, 64);
            textBox.ShowScrollBars = false;
            textBox.Size = new Size(150, 150);
            textBox.TabIndex = 2;
            textBox.Text = "fastColoredTextBox1";
            textBox.Zoom = 100;

            menuStrip.BackColor = Color.FromArgb(46,46,46);
            menuStrip.BackgroundImageLayout = ImageLayout.None;
            menuStrip.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point,134);
            menuStrip.ForeColor = Color.FromArgb(48,48,48);
            menuStrip.Items.AddRange(new ToolStripItem[] {menuItemCopy,menuItemCopyAll});
            menuStrip.Name = "uiContextMenuStrip1";
            menuStrip.ShowImageMargin = false;
            menuStrip.ShowItemToolTips = false;
            menuStrip.Size = new Size(120, 56);
            menuStrip.Style = UIStyle.Custom;
            menuStrip.StyleCustomMode = true;

            menuItemCopy.ForeColor = Color.FromArgb(224, 224, 224);
            menuItemCopy.Name = "MenuItemCopy";
            menuItemCopy.Size = new Size(119, 26);
            menuItemCopy.Text = "复制选中";
            menuItemCopy.Click += new EventHandler(MenuItemCopy_Click);

            menuItemCopyAll.ForeColor = Color.FromArgb(224, 224, 224);
            menuItemCopyAll.Name = "MenuItemCopyAll";
            menuItemCopyAll.Size = new Size(119, 26);
            menuItemCopyAll.Text = "复制所有";
            menuItemCopyAll.Click += new EventHandler(MenuItemCopyAll_Click);


            textBox.Scroll += (sender, e) => { RefreshVBarValue(); };
            textBox.ClientSizeChanged += (sender, e) => { SetScrollInfo(); };
            textBox.MouseWheel += TextBox_MouseWheel;
            VBar.ValueChanged += (sender, e) => { SetPanelVBarValue(); };

            Controls.Add(textBox);
            Controls.Add(VBar);
        }
        public void Add(Control control)
        {
            textBox.Controls.Add(control);
            SetScrollInfo();
        }
        public void Remove(Control control)
        {
            textBox.Controls.Remove(control);
            SetScrollInfo();
        }
        public void SetScrollInfo()
        {
            if (VBar != null)
            {
                if (textBox.VerticalScroll.Visible)
                {
                    VBar.Maximum = textBox.VerticalScroll.Maximum - textBox.VerticalScroll.LargeChange - textBox.VerticalScroll.Minimum;
                    VBar.Value = textBox.VerticalScroll.Value;
                    //VBar = base.VerticalScroll.LargeChange;
                    //VBar.LargeChange = base.VerticalScroll.LargeChange;
                }
                VBar.Visible = textBox.VerticalScroll.Visible;
                SetBarPosition();
            }
        }
        private void SetBarPosition()
        {
            if (VBar != null)
            {
                VBar.Left = base.Width - ScrollBarInfo.VerticalScrollBarWidth() - 1;
                VBar.Top = 0;
                VBar.Width = ScrollBarInfo.VerticalScrollBarWidth() + 1;
                VBar.Height = base.Height;
                VBar.BringToFront();
            }
        }
        private void RefreshVBarValue()
        {
            VBar.Value = textBox.VerticalScroll.Value;
        }
        private void SetPanelVBarValue()
        {
            var mMax = textBox.VerticalScroll.Maximum - textBox.VerticalScroll.LargeChange;
            if (VBar.Value.InRange(0, mMax))
            {
                if (mMax > 0)
                {
                    //计算百分比
                    var rate = VBar.Value * 1.0 / VBar.Maximum;
                    int mValue = Convert.ToInt32(rate * mMax);
                    textBox.VerticalScroll.Value = mValue;
                }
            }
        }
        private void TextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            RefreshVBarValue();
        }

        private void MenuItemCopyAll_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.Text);
            UIMessageTip.ShowOk("已将所有文本复制到剪贴板");
        }
        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            textBox.Copy();
            UIMessageTip.ShowOk("已将选中文本复制到剪贴板");
        }
    }
}
