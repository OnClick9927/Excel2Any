using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UICustomPanel : Panel, IToolTip
    {
        private UIScrollBar VBar;

        private UIHorScrollBarEx HBar;

        private FlowLayoutPanel flowLayoutPanel;

        private readonly Timer timer;

        private Color scrollBarColor = Color.FromArgb(80, 160, 255);

        public override bool Focused => Panel.Focused;

        [DefaultValue(FlowDirection.LeftToRight)]
        [Localizable(true)]
        public FlowDirection FlowDirection
        {
            get
            {
                return Panel.FlowDirection;
            }
            set
            {
                Panel.FlowDirection = value;
            }
        }

        [DefaultValue(true)]
        [Localizable(true)]
        public bool WrapContents
        {
            get
            {
                return Panel.WrapContents;
            }
            set
            {
                Panel.WrapContents = value;
            }
        }

        [Browsable(false)]
        public FlowLayoutPanel FlowLayoutPanel => flowLayoutPanel;

        [Description("滚动条填充颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get
            {
                return scrollBarColor;
            }
            set
            {
                scrollBarColor = value;
                VBar.ForeColor = value;
                HBar.ForeColor = value;
                Invalidate();
            }
        }

        public FlowLayoutPanel Panel => flowLayoutPanel;

        public new event ScrollEventHandler Scroll;

        public UICustomPanel()
        {
            InitializeComponent();
            Panel.AutoScroll = true;
            Panel.ControlAdded += Panel_ControlAdded;
            Panel.ControlRemoved += Panel_ControlRemoved;
            Panel.Scroll += Panel_Scroll;
            Panel.MouseWheel += Panel_MouseWheel;
            Panel.MouseEnter += Panel_MouseEnter;
            Panel.MouseClick += Panel_MouseClick;
            Panel.ClientSizeChanged += Panel_ClientSizeChanged;
            Panel.BackColor = Color.FromArgb(30, 30, 30);
            VBar.ValueChanged += VBar_ValueChanged;
            HBar.ValueChanged += HBar_ValueChanged;
            base.SizeChanged += Panel_SizeChanged;
            timer = new Timer
            {
                Interval = 100
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (flowLayoutPanel != null)
            {
                flowLayoutPanel.Font = Font;
            }

            if (VBar != null)
            {
                VBar.Font = Font;
            }

            if (HBar != null)
            {
                HBar.Font = Font;
            }
        }

        public Control ExToolTipControl()
        {
            return Panel;
        }

        public new void Focus()
        {
            base.Focus();
            Panel.Focus();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Panel.Focus();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer?.Stop();
            timer?.Dispose();
        }

        [DefaultValue(false)]
        [DisplayName("FlowBreak")]
        public bool GetFlowBreak(Control control)
        {
            return Panel.GetFlowBreak(control);
        }

        [DisplayName("FlowBreak")]
        public void SetFlowBreak(Control control, bool value)
        {
            Panel.SetFlowBreak(control, value);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            UIHorScrollBarEx uIHorScrollBarEx = e.Control as UIHorScrollBarEx;
            if (uIHorScrollBarEx != null && uIHorScrollBarEx.TagString == "79E1E7DD-3E4D-916B-C8F1-F45B579C290C")
            {
                base.OnControlAdded(e);
                return;
            }

            UIVerScrollBarEx uIVerScrollBarEx = e.Control as UIVerScrollBarEx;
            if (uIVerScrollBarEx != null && uIVerScrollBarEx.TagString == "63FD1249-41D3-E08A-F8F5-CC41CC30FD03")
            {
                base.OnControlAdded(e);
                return;
            }

            FlowLayoutPanel flowLayoutPanel = e.Control as FlowLayoutPanel;
            if (flowLayoutPanel != null && flowLayoutPanel.Tag.ToString() == "69605093-6397-AD32-9F69-3C29F642F87E")
            {
                base.OnControlAdded(e);
                return;
            }

            base.OnControlAdded(e);
            if (Panel != null)
            {
                Panel.SendToBack();
            }
        }
        public void Remove(Control control)
        {
            if (Panel != null && Panel.Controls.Contains(control))
            {
                Panel.Controls.Remove(control);
            }
            SetScrollInfo();
        }

        public void Add(Control control)
        {
            if (Panel != null)
            {
                Panel.Controls.Add(control);
            }
            SetScrollInfo();
        }

        public void Clear()
        {
            foreach (Control control in Panel.Controls)
            {
                control.Dispose();
            }

            Panel.Controls.Clear();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (VBar.Maximum != Panel.VerticalScroll.Maximum || VBar.Visible != Panel.VerticalScroll.Visible || HBar.Maximum != Panel.HorizontalScroll.Maximum || HBar.Visible != Panel.HorizontalScroll.Visible)
            {
                SetScrollInfo();
            }
        }

        private void Panel_ClientSizeChanged(object sender, EventArgs e)
        {
            SetScrollInfo();
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Panel.Focus();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Panel.Focus();
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (Panel.VerticalScroll.Maximum > Panel.VerticalScroll.Value + 50)
                {
                    Panel.VerticalScroll.Value += 50;
                }
                else
                {
                    Panel.VerticalScroll.Value = Panel.VerticalScroll.Maximum;
                }
            }
            else if (Panel.VerticalScroll.Value > 50)
            {
                Panel.VerticalScroll.Value -= 50;
            }
            else
            {
                Panel.VerticalScroll.Value = 0;
            }

            VBar.Value = Panel.VerticalScroll.Value;
        }

        private void VBar_ValueChanged(object sender, EventArgs e)
        {
            if (VBar.Value.InRange(0, Panel.VerticalScroll.Maximum))
            {
                Panel.VerticalScroll.Value = VBar.Value / 2;
            }
        }

        private void HBar_ValueChanged(object sender, EventArgs e)
        {
            Panel.HorizontalScroll.Value = HBar.Value;
        }

        private void Panel_Scroll(object sender, ScrollEventArgs e)
        {
            this.Scroll?.Invoke(this, e);
            VBar.Value = Panel.VerticalScroll.Value;
        }

        private void Panel_SizeChanged(object sender, EventArgs e)
        {
            SetScrollInfo();
        }

        private void Panel_ControlRemoved(object sender, ControlEventArgs e)
        {
            SetScrollInfo();
        }

        private void Panel_ControlAdded(object sender, ControlEventArgs e)
        {
            SetScrollInfo();
        }

        public void SetScrollInfo()
        {
            VBar.Visible = Panel.VerticalScroll.Visible;
            VBar.Maximum = Panel.VerticalScroll.Maximum;
            VBar.Value = Panel.VerticalScroll.Value;


            HBar.Visible = Panel.HorizontalScroll.Visible;
            HBar.Maximum = Panel.HorizontalScroll.Maximum;
            HBar.Value = Panel.HorizontalScroll.Value;
            HBar.LargeChange = Panel.HorizontalScroll.LargeChange;
            HBar.BoundsWidth = Panel.HorizontalScroll.LargeChange;
            SetScrollPos();
        }

        private void InitializeComponent()
        {
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            VBar = new Sunny.UI.UIScrollBar();
            HBar = new Sunny.UI.UIHorScrollBarEx();
            SuspendLayout();
            flowLayoutPanel.BackColor = System.Drawing.Color.FromArgb(235, 243, 255);
            flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel.Location = new System.Drawing.Point(2, 2);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(429, 383);
            flowLayoutPanel.TabIndex = 0;
            flowLayoutPanel.Tag = "69605093-6397-AD32-9F69-3C29F642F87E";

            VBar.Parent = this;
            VBar.Visible = false;
            VBar.ForeColor = UIColor.Blue;
            VBar.StyleCustomMode = true;
            VBar.ShowLeftLine = false;


            VBar.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            VBar.Location = new System.Drawing.Point(410, 5);
            VBar.MinimumSize = new System.Drawing.Size(1, 1);
            VBar.Name = "VBar";
            VBar.Size = new System.Drawing.Size(18, 377);
            VBar.TabIndex = 1;
            VBar.TagString = "63FD1249-41D3-E08A-F8F5-CC41CC30FD03";
            VBar.Text = "uiVerScrollBarEx1";
            VBar.Value = 0;
            VBar.FillColor = Color.FromArgb(30, 30, 30);

            VBar.Visible = false;
            HBar.BoundsWidth = 10;
            HBar.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            HBar.LargeChange = 10;
            HBar.Location = new System.Drawing.Point(5, 364);
            HBar.MinimumSize = new System.Drawing.Size(1, 1);
            HBar.Name = "HBar";
            HBar.Size = new System.Drawing.Size(399, 18);
            HBar.TabIndex = 2;
            HBar.TagString = "79E1E7DD-3E4D-916B-C8F1-F45B579C290C";
            HBar.Text = "uiHorScrollBarEx1";
            HBar.Value = 0;
            HBar.Visible = false;

            SetScrollPos();

            base.Controls.Add(HBar);
            base.Controls.Add(VBar);
            base.Controls.Add(flowLayoutPanel);
            base.Name = "CustomPanel";
            base.Padding = new System.Windows.Forms.Padding(2);
            base.Size = new System.Drawing.Size(433, 387);
            ResumeLayout(false);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollPos();
        }

        private void SetScrollPos()
        {
            if (VBar != null && HBar != null)
            {
                int num = 1;

                VBar.Left = base.Width - ScrollBarInfo.VerticalScrollBarWidth() - 2;
                VBar.Top = 1;
                VBar.Width = ScrollBarInfo.VerticalScrollBarWidth() + 1;
                VBar.Height = base.Height - 2;
                VBar.BringToFront();


                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight();
                HBar.Left = num;
                HBar.Top = base.Height - HBar.Height - num;
                if (VBar.Visible)
                {
                    HBar.Width = VBar.Left - 1 - num;
                }
                else
                {
                    HBar.Width = base.Width - num * 2;
                }
            }
        }
    }
}