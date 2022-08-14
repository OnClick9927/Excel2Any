using System;
using System.Data;

namespace Excel2Any.Winform
{
    public partial class CSVConvertPage : DataConvertPage
    {
        public CSVConvertPage()
        {
            InitializeComponent();
            grdData.ColumnHeadersVisible = false;
            grdData.RowHeadersVisible = false;
        }

        protected override void tabSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSheets.SelectedIndex == -1) return;
            tabSheets.TabPages[tabSheets.SelectedIndex].Controls.Add(grdData);

            if (tabSheets.SelectedIndex <= _sheets.Count - 1)
            {
                grdData.DataSource = GetData(_sheets[tabSheets.SelectedIndex].content.ToString());
            }
        }

        public override void RefreshSheet()
        {
            grdData.Visible = false;

            if (RefreshTab())
            {
                grdData.Visible = true;
                tabSheets.TabPages[0].Controls.Add(grdData);
                grdData.DataSource = GetData(_sheets[tabSheets.SelectedIndex].content.ToString());
                tabSheets.SelectTab(0);
            }
        }


        private DataTable GetData(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            DataTable dt = new DataTable();

            var rows = str.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Equals("") || rows[i].Equals("\r")) continue;
                var item = rows[i].Replace("\r", "");
                if (item.EndsWith(","))
                {
                    item = item.Substring(0, item.Length - 1);
                }

                var cols = item.Split(',');
                if (dt.Columns.Count <= 0 && cols.Length > 0)
                {
                    for (int j = 0; j < cols.Length; j++)
                    {
                        dt.Columns.Add("");
                    }
                }

                DataRow row = dt.NewRow();
                for (int j = 0; j < cols.Length; j++)
                {
                    row[j] = cols[j];
                }
                dt.Rows.Add(row);

            }
            return dt;
        }

    }
}
