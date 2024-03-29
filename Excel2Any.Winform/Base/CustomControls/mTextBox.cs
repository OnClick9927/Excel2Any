﻿using FastColoredTextBoxNS;
using Sunny.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public partial class mTextBox : Panel
    {

        UIScrollBar VBar = new UIScrollBar();
        UIHorScrollBar HBar = new UIHorScrollBar();
        FastColoredTextBox textBox = new FastColoredTextBox();

        UIContextMenuStrip menuStrip = new UIContextMenuStrip();
        ToolStripMenuItem menuItemCopy = new ToolStripMenuItem();
        ToolStripMenuItem menuItemCopyAll = new ToolStripMenuItem();

        private readonly Timer timer;

        public new string Text { get { return textBox.Text; } set { textBox.Text = value; } }
        public char LeftBracket { get { return textBox.LeftBracket; } set { textBox.LeftBracket = value; } }
        public char RightBracket { get { return textBox.RightBracket; } set { textBox.RightBracket = value; } }
        public char LeftBracket2 { get { return textBox.LeftBracket2; } set { textBox.LeftBracket2 = value; } }
        public char RightBracket2 { get { return textBox.RightBracket2; } set { textBox.RightBracket2 = value; } }
        public string CommentPrefix { get { return textBox.CommentPrefix; } set { textBox.CommentPrefix = value; } }
        public string AutoIndentCharsPatterns { get { return textBox.AutoIndentCharsPatterns; } set { textBox.AutoIndentCharsPatterns = value; } }

        public new EventHandler<TextChangedEventArgs> TextChanged;

        public Line this[int index]
        {
            get { return textBox[index]; }
        }

        public mTextBox()
        {
            InitializeComponent();
            VBar.Parent = this;
            VBar.StyleCustomMode = true;
            VBar.FillColor = Color.FromArgb(30, 30, 30);
            VBar.ForeColor = Color.FromArgb(66, 66, 66);
            VBar.HoverColor = Color.FromArgb(79, 79, 79);
            VBar.PressColor = Color.FromArgb(94, 94, 94);
            VBar.ShowLeftLine = false;

            HBar.Parent = this;
            HBar.StyleCustomMode = true;
            HBar.FillColor = Color.FromArgb(30, 30, 30);
            HBar.ForeColor = Color.FromArgb(66, 66, 66);
            HBar.HoverColor = Color.FromArgb(79, 79, 79);
            HBar.PressColor = Color.FromArgb(94, 94, 94);

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
            textBox.Dock = DockStyle.Fill;
            textBox.ForeColor = Color.FromArgb(212, 212, 212);
            textBox.ImeMode = ImeMode.Off;
            textBox.IndentBackColor = Color.FromArgb(30, 30, 30);
            textBox.IsReplaceMode = false;
            textBox.LineNumberColor = Color.FromArgb(133, 133, 133);
            textBox.Language = Language.Custom;
            textBox.Location = new Point(50, 50);
            textBox.Paddings = new Padding(0);
            textBox.SelectionColor = Color.FromArgb(60, 38, 79, 200);
            textBox.ServiceLinesColor = Color.FromArgb(64, 64, 64);
            textBox.Size = new Size(150, 150);
            textBox.Zoom = 100;
            textBox.TextChanged += (sender, e) =>
            {
                TextChanged?.Invoke(sender, e);
            };

            menuStrip.BackColor = Color.FromArgb(46, 46, 46);
            menuStrip.BackgroundImageLayout = ImageLayout.None;
            menuStrip.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            menuStrip.ForeColor = Color.FromArgb(48, 48, 48);
            menuStrip.Items.AddRange(new ToolStripItem[] { menuItemCopy, menuItemCopyAll });
            menuStrip.ShowImageMargin = false;
            menuStrip.ShowItemToolTips = false;
            menuStrip.Size = new Size(120, 56);
            menuStrip.Style = UIStyle.Custom;
            menuStrip.StyleCustomMode = true;

            menuItemCopy.ForeColor = Color.FromArgb(224, 224, 224);
            menuItemCopy.Size = new Size(119, 26);
            menuItemCopy.Text = "复制选中";
            menuItemCopy.Click += new EventHandler(MenuItemCopy_Click);

            menuItemCopyAll.ForeColor = Color.FromArgb(224, 224, 224);
            menuItemCopyAll.Size = new Size(119, 26);
            menuItemCopyAll.Text = "复制所有";
            menuItemCopyAll.Click += new EventHandler(MenuItemCopyAll_Click);


            textBox.Scroll += (sender, e) => { RefreshBarValue(); };
            textBox.ClientSizeChanged += (sender, e) => { SetScrollInfo(); };
            textBox.MouseWheel += TextBox_MouseWheel;
            VBar.ValueChanged += (sender, e) => { SetPanelVBarValue(); };
            HBar.ValueChanged += (sender, e) => { SetPanelHBarValue(); };

            VBar.MouseCaptureChanged += (sender, e) => { textBox.Refresh(); };
            HBar.MouseCaptureChanged += (sender, e) => { textBox.Refresh(); };

            Controls.Add(textBox);
            Controls.Add(VBar);


            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (VBar.Maximum != textBox.VerticalScroll.Maximum || VBar.Visible != textBox.VerticalScroll.Visible)
            {
                SetScrollInfo();
            }
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
                    VBar.Maximum = textBox.VerticalScroll.Maximum - textBox.Size.Height - textBox.VerticalScroll.Minimum;
                    VBar.Value = textBox.VerticalScroll.Value;
                    //VBar = base.VerticalScroll.LargeChange;
                    //VBar.LargeChange = base.VerticalScroll.LargeChange;
                }
                VBar.Visible = textBox.VerticalScroll.Visible;
                
            }

            if (HBar != null)
            {
                if (textBox.HorizontalScroll.Visible)
                {
                    HBar.Maximum = textBox.HorizontalScroll.Maximum - textBox.Size.Width - textBox.HorizontalScroll.Minimum;
                    HBar.Value = textBox.HorizontalScroll.Value;
                }
                HBar.Visible = textBox.HorizontalScroll.Visible;
            }

            SetBarPosition();
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


            if (HBar != null)
            {
                HBar.Left = 0;
                HBar.Top = base.Height - ScrollBarInfo.HorizontalScrollBarHeight() - 1;
                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;
                HBar.Width = base.Width;
                HBar.BringToFront();
            }
        }
        private void RefreshBarValue()
        {
            VBar.Value = textBox.VerticalScroll.Value;
            HBar.Value = textBox.HorizontalScroll.Value;
        }
        private void SetPanelVBarValue()
        {
            textBox.VerticalScroll.Value = VBar.Value;
        }

        private void SetPanelHBarValue()
        {
            textBox.HorizontalScroll.Value = HBar.Value;
        }

        private void TextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            RefreshBarValue();
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
