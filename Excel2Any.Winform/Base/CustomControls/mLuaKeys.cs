using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Any.Winform.Base.CustomControls
{
    public partial class mLuaKeys : UIDataGridView
    {
        public Action<HashSet<string>> OnKeyChange;
        public mLuaKeys()
        {
            InitializeComponent();


            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;

            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            this.BackgroundColor = Color.FromArgb(30, 30, 30);
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.EnableHeadersVisualStyles = false;
            this.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.GridColor = Color.FromArgb(88, 88, 88);
            this.MultiSelect = false;
            this.ReadOnly = true;
            this.RectColor = Color.FromArgb(88, 88, 88);
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ScrollBarBackColor = Color.FromArgb(30, 30, 30);
            this.ScrollBarColor = Color.FromArgb(66, 66, 66);
            this.ScrollBarRectColor = Color.FromArgb(30, 30, 30);
            this.ShowCellToolTips = false;
            this.ShowEditingIcon = false;
            this.ShowRowErrors = false;
            this.StripeEvenColor = Color.FromArgb(37, 37, 38);
            this.Style = UIStyle.Custom;
            this.StyleCustomMode = true;
            this.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.grdData_RowPostPaint);
            this.SelectionMode = DataGridViewSelectionMode.CellSelect;

            DataGridViewCellStyle columnHeadersDefaultCellStyle = new DataGridViewCellStyle();
            columnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            columnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            columnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(204, 204, 204);
            columnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 55, 61);
            columnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(212, 212, 212);
            columnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.ColumnHeadersDefaultCellStyle = columnHeadersDefaultCellStyle;

            this.RowHeadersVisible = false;

            this.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 37, 38);

            this.CellClick += grdData_CellClick;

        }

        private void grdData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.RowHeadersVisible)
            {
                e.PaintHeader(DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                SolidBrush solidBrush = new SolidBrush(this.RowHeadersDefaultCellStyle.ForeColor);
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


        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var col = this.Columns[e.ColumnIndex];
                // 切换图片的显示状态
                if ((bool)col.Tag)
                {
                    cell.Value = null;
                    col.Tag = false;
                }
                else
                {
                    cell.Value = Properties.Resources.key;
                    col.Tag = true;
                }

                var keyList = new HashSet<string>();

                for (int i = 0; i < this.Columns.Count; i++)
                {
                    if ((bool)this.Columns[i].Tag)
                    {
                        keyList.Add(this.Columns[i].DataPropertyName);
                    }
                }
                OnKeyChange.Invoke(keyList);
            }
        }
    }
}
