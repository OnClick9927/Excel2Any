using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public partial class mPanel : Panel
    {
        UIScrollBar VBar = new UIScrollBar();
        Panel panel = new Panel();
        public mPanel()
        {
            InitializeComponent();
            panel.Dock = DockStyle.Fill;
            VBar.Parent = this;
            VBar.StyleCustomMode = true;
            VBar.FillColor = Color.FromArgb(30, 30, 30);
            VBar.ForeColor = Color.FromArgb(66, 66, 66);
            VBar.HoverColor = Color.FromArgb(79, 79, 79);
            VBar.PressColor = Color.FromArgb(94, 94, 94);
            VBar.ShowLeftLine = false;

            panel.AutoScroll = true;
            panel.Scroll += (sender, e) => { RefreshVBarValue(); };
            panel.ClientSizeChanged += (sender, e) => { SetScrollInfo(); };
            panel.MouseWheel += Panel_MouseWheel;
            VBar.ValueChanged += (sender, e) => { SetPanelVBarValue(); };
            this.Controls.Add(panel);
            this.Controls.Add(VBar);
        }
        public void Add(Control control)
        {
            panel.Controls.Add(control);
            SetScrollInfo();
        }
        public void Remove(Control control)
        {
            panel.Controls.Remove(control);
            SetScrollInfo();
        }
        public void SetScrollInfo()
        {
            if (VBar != null)
            {
                if (panel.VerticalScroll.Visible)
                {
                    VBar.Maximum = panel.VerticalScroll.Maximum - panel.VerticalScroll.LargeChange - panel.VerticalScroll.Minimum;
                    VBar.Value = panel.VerticalScroll.Value;
                    //VBar = base.VerticalScroll.LargeChange;
                    //VBar.LargeChange = base.VerticalScroll.LargeChange;
                }
                VBar.Visible = panel.VerticalScroll.Visible;
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
            VBar.Value = panel.VerticalScroll.Value;
        }
        private void SetPanelVBarValue()
        {
            var mMax = panel.VerticalScroll.Maximum - panel.VerticalScroll.LargeChange;
            if (VBar.Value.InRange(0, mMax))
            {
                if (mMax > 0)
                {
                    //计算百分比
                    var rate = VBar.Value * 1.0 / VBar.Maximum;
                    int mValue = Convert.ToInt32(rate * mMax);
                    panel.VerticalScroll.Value = mValue;
                }
            }
        }
        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (e.Delta < 0)
            //{
            //    if (panel.VerticalScroll.Maximum > panel.VerticalScroll.Value + 50)
            //    {
            //        panel.VerticalScroll.Value += 50;
            //    }
            //    else
            //    {
            //        panel.VerticalScroll.Value = panel.VerticalScroll.Maximum;
            //    }
            //}
            //else if (panel.VerticalScroll.Value > 50)
            //{
            //    panel.VerticalScroll.Value -= 50;
            //}
            //else
            //{
            //    panel.VerticalScroll.Value = 0;
            //}

            RefreshVBarValue();
        }

        public List<Control> GetAllControl()
        {
            List<Control> controls = new List<Control>();
            GetControls(panel, controls);
            return controls;
        }

        private void GetControls(Control fatherControl,List<Control> controls)
        {
            ControlCollection sonControls = fatherControl.Controls;
            //遍历所有控件
            foreach (Control control in sonControls)
            {
                var type = control.GetType();
                if (type == typeof(UITextBox) || type== typeof(UISwitch))
                {
                    controls.Add(control);
                }
                if (control.Controls != null)
                {
                    GetControls(control, controls);
                }
            }
        }
    }
}
