using Excel2Any.Winform.Base.CustomControls;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public partial class LuaConvertPage : BaseConvertPage
    {
        protected mTextBox txtCode = new mTextBox();
        protected TableLayoutPanel pnlLua = new TableLayoutPanel();
        protected mLuaKeys grdData = new mLuaKeys();
        public LuaConvertPage()
        {
            InitializeComponent();

            grdData.Parent = pnlLua;
            txtCode.Parent = pnlLua;

            grdData.Height = 80;

            pnlLua.Dock = DockStyle.Fill;

            grdData.Dock = DockStyle.Top;
            txtCode.Dock = DockStyle.Fill;
            txtCode.TextChanged += txtCode_TextChanged;
            grdData.OnKeyChange += SaveKeys;
        }


        public override void tabSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSheets.SelectedIndex == -1) return;
            tabSheets.TabPages[tabSheets.SelectedIndex].Controls.Add(pnlLua);
            RefreshDataGrid(_rows[tabSheets.SelectedIndex]);
            txtCode.Text = _sheets[tabSheets.SelectedIndex].content.ToString();
        }

        public override void RefreshSheet()
        {
            pnlLua.Visible = false;
            if (RefreshTab())
            {
                tabSheets.TabPages[0].Controls.Add(pnlLua);
                RefreshDataGrid(_rows[0]);
                txtCode.Text = _sheets[0].content.ToString();

                pnlLua.Visible = true;
            }
        }

        private void RefreshDataGrid(List<RowHead> rows)
        {
            grdData.ClearAll();

            DataGridViewRow row = new DataGridViewRow();

            for (int i = 0; i < rows.Count; i++)
            {
                var fieldName = rows[i].fieldName;
                var isKey = rows[i].isKey;
                var col = grdData.AddColumn(fieldName, fieldName);
                col.Tag = isKey;
                col.DefaultCellStyle.NullValue = null;

                DataGridViewImageCell imageCell = new DataGridViewImageCell();
                imageCell.Value = isKey ? Properties.Resources.key : null;
                row.Cells.Add(imageCell);
            }

            grdData.Rows.Add(row);
        }


        public void SaveKeys(HashSet<string> keys)
        {
            //将文件用Json序列化至文件目录下的.keys文件夹下的同名文件
            var keyDir = Path.GetDirectoryName(_path) + "/.keys";
            var fileName = Path.GetFileNameWithoutExtension(_path);
            var keyFileDir = Path.Combine(keyDir, fileName);
            if (!Directory.Exists(keyDir))
            {
                var dir = Directory.CreateDirectory(keyDir);
                dir.Attributes |= FileAttributes.Hidden;
            }

            using (FileStream file = new FileStream(keyFileDir, FileMode.Create, FileAccess.Write))
            {
                var encoding = new UTF8Encoding(false);
                using (TextWriter writer = new StreamWriter(file, encoding))
                {
                    var str = JsonConvert.SerializeObject(keys);
                    writer.Write(str);
                }
            }
            ExcelHelper.SetHistoryDirty(_path);
        }
    }
}
