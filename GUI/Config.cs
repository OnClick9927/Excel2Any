using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace excel2other.GUI
{
    public partial class Config : Form
    {
        public Options options;
        public Config(Options options)
        {
            InitializeComponent();
            this.options = options;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnSave.DialogResult = DialogResult.OK;

            comboBoxType.SelectedIndex = options.ExportArray ? 0 : 1;
            comboBoxLowcase.SelectedIndex = options.Lowcase ? 0 : 1;
            comboBoxHeader.Text = options.HeaderRows.ToString();
            comboBoxDateFormat.Text = options.DateFormat;
            comboBoxSheetName.SelectedIndex = options.ForceSheetName ? 0 : 1;

            comboBoxEncoding.Items.Clear();
            comboBoxEncoding.Items.Add("utf8-nobom");
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();
                comboBoxEncoding.Items.Add(e.HeaderName);
            }
            comboBoxEncoding.SelectedIndex = 0;

            comboBoxEncoding.Text= options.Encoding;

            txtOutputPath.Text = options.OutputPath;
            txtInPath.Text = options.InPath;
            checkBoxCellJson.Checked = options.CellJson;
            checkBoxAllString.Checked = options.AllString;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            options.ExportArray = comboBoxType.SelectedIndex == 0;
            options.Encoding = comboBoxEncoding.Text;
            options.Lowcase = comboBoxLowcase.SelectedIndex == 0;
            options.HeaderRows = int.Parse(comboBoxHeader.Text);
            options.DateFormat = comboBoxDateFormat.Text;
            options.ForceSheetName = comboBoxSheetName.SelectedIndex == 0;
            options.ExcludePrefix = textBoxExculdePrefix.Text;
            options.CellJson = checkBoxCellJson.Checked;
            options.AllString = checkBoxAllString.Checked;

            options .InPath= txtInPath.Text;
            options.OutputPath= txtOutputPath.Text;
            this.Close();
        }

        private void txtInPath_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "请选择文件夹路径";
            dlg.SelectedPath= txtInPath.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtInPath.Text= dlg.SelectedPath;
            }
        }

        private void txtOutputPath_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "请选择文件夹路径";
            dlg.SelectedPath = txtOutputPath.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = dlg.SelectedPath;
            }
        }
    }
}
