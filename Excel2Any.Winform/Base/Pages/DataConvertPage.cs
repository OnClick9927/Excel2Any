using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public partial class DataConvertPage : BaseConvertPage
    {
        public DataConvertPage()
        {
            InitializeComponent();
            grdData.Dock = DockStyle.Fill;
            grdData.Visible = false;
        }

        protected virtual void tabSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSheets.SelectedIndex == -1) return;
            tabSheets.TabPages[tabSheets.SelectedIndex].Controls.Add(grdData);

            //这里为了防止在清空节点重新添加途中报错所以无脑判断了一下是否越界
            if (tabSheets.SelectedIndex <= _sheets.Count - 1)
            {
                //grdData.ClearAll();
                grdData.DataSource = ((DataContent)_sheets[tabSheets.SelectedIndex].content).value;
            }
        }

        /// <summary>
        /// 刷新Sheet
        /// </summary>
        public override void RefreshSheet()
        {
            grdData.Visible = false;

            if (RefreshTab())
            {
                grdData.Visible = true;
                tabSheets.TabPages[0].Controls.Add(grdData);
                //grdData.ClearAll();
                var data = ((DataContent)_sheets[0].content).value;
                grdData.DataSource = data;
                tabSheets.SelectTab(0);
            }
        }

        private void grdData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (grdData.RowHeadersVisible)
            {
                e.PaintHeader(DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                SolidBrush solidBrush = new SolidBrush(grdData.RowHeadersDefaultCellStyle.ForeColor);
                int xh = e.RowIndex + 1;
                //设置显示数字的最大长度
                var xhStr = xh.ToString();
                if (xhStr.Length > 2)
                {
                    xhStr = xhStr.Substring(0, 2) + "...";
                }
                e.Graphics.DrawString(xhStr, e.InheritedRowStyle.Font,
                    solidBrush, e.RowBounds.Location.X, e.RowBounds.Location.Y);
            }
        }
    }
}
