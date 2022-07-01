using Microsoft.WindowsAPICodePack.Dialogs;
using Sunny.UI;
using System;
using System.IO;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public partial class SettingPage : UIPage
    {

        private JsonSettings _jsonSetting;
        private CSharpSettings _cSharpSetting;
        private XmlSettings _xmlSetting;
        private FormSetting _formSetting;

        public Action<bool> onJsonSave;
        public Action<bool> onXmlSave;
        public Action<bool> onCSharpSave;
        public Action onFormSave;

        public Action onJsonLoad;
        public Action onXmlLoad;
        public Action onCSharpLoad;

        public Action<bool> onNavMenuChange;

        public SettingPage(JsonSettings jsonSetting, CSharpSettings cSharpSetting, XmlSettings xmlSetting, FormSetting formSetting)
        {
            _jsonSetting = jsonSetting;
            _cSharpSetting = cSharpSetting;
            _xmlSetting = xmlSetting;
            _formSetting = formSetting;
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
            InitFormSettings();
            InitJsonSettings();
            InitXmlSettings();
            InitCSharpSettings();

        }

        /// <summary>
        /// 窗体配置
        /// </summary>
        public void InitFormSettings()
        {
            if (_formSetting != null)
            {
                swComExcludeFile.Active = _formSetting.excludeFile;
                swComLast.Active = _formSetting.openLast;
                txtComPrefix.Text = _formSetting.excludePrefix;
                onNavMenuChange?.Invoke(_formSetting.isExpand);
            }
        }

        public void InitJsonSettings()
        {
            if (_jsonSetting != null)
            {
                swJsonCell.Active = _jsonSetting.JsonCell;
                swJsonString.Active = _jsonSetting.allString;
                swJsonSpace.Active = _jsonSetting.saveSpace;
                txtJsonDateFormat.Text = _jsonSetting.dateFormat;
                txtJsonSavePath.Text = _jsonSetting.savePath;
                swJsonMultiFiles.Active = _jsonSetting.separateBySheet;
                txtJsonExcludePrefix.Text = _jsonSetting.excludePrefix;
                swJsonExcludeSheet.Active = _jsonSetting.excludeSheet;
                txtJsonField.Text = (_jsonSetting.FieldRowNum + 1).ToString();
                txtJsonStart.Text = (_jsonSetting.StartRowNum + 1).ToString();
                swJsonFirstCol.Active = _jsonSetting.excludeFirstCol;
            }
        }


        public void InitXmlSettings()
        {
            if (_xmlSetting != null)
            {
                txtXmlDateFormat.Text = _xmlSetting.dateFormat;
                txtXmlSavePath.Text = _xmlSetting.savePath;
                swXmlMultiFiles.Active = _xmlSetting.separateBySheet;
                txtXmlExcludePrefix.Text = _xmlSetting.excludePrefix;
                swXmlExcludeSheet.Active = _xmlSetting.excludeSheet;
                txtXmlComment.Text = (_xmlSetting.CommentRowNum + 1).ToString();
                txtXmlType.Text = (_xmlSetting.TypeRowNum + 1).ToString();
                txtXmlField.Text = (_xmlSetting.FieldRowNum + 1).ToString();
                txtXmlStart.Text = (_xmlSetting.StartRowNum + 1).ToString();
                swXmlFirstCol.Active = _xmlSetting.excludeFirstCol;
            }
        }

        public void InitCSharpSettings()
        {
            if (_cSharpSetting != null)
            {
                txtCsSavePath.Text = _cSharpSetting.savePath;
                swCsMultiFiles.Active = _cSharpSetting.separateBySheet;
                txtCsExcludePrefix.Text = _cSharpSetting.excludePrefix;
                swCsExcludeSheet.Active = _cSharpSetting.excludeSheet;
                txtCsComment.Text = (_cSharpSetting.CommentRowNum + 1).ToString();
                txtCsType.Text = (_cSharpSetting.TypeRowNum + 1).ToString();
                txtCsField.Text = (_cSharpSetting.FieldRowNum + 1).ToString();
                swCsFirstCol.Active = _cSharpSetting.excludeFirstCol;
                swCsProperty.Active = _cSharpSetting.IsProperty;
            }
        }


        /// <summary>
        /// 打开对应设置
        /// </summary>
        /// <param name="tabName">标签页名</param>
        public void OpenSetting(string tabName)
        {
            for (int i = 0; i < tabSettings.TabPages.Count; i++)
            {
                if (tabName.Equals(tabSettings.TabPages[i].Text))
                {
                    tabSettings.SelectTab(i);
                    return;
                }
            }
        }

        private void btnJsonLoad_Click(object sender, EventArgs e)
        {
            onJsonLoad?.Invoke();
        }

        private void btnJsonSave_Click(object sender, EventArgs e)
        {
            onJsonSave?.Invoke(true);
        }

        private void btnXmlLoad_Click(object sender, EventArgs e)
        {
            onXmlLoad?.Invoke();
        }

        private void btnXmlSave_Click(object sender, EventArgs e)
        {
            onXmlSave?.Invoke(true);
        }

        private void btnCsLoad_Click(object sender, EventArgs e)
        {
            onCSharpLoad?.Invoke();
        }

        private void btnCsSave_Click(object sender, EventArgs e)
        {
            onCSharpSave?.Invoke(true);
        }

        private void swComLast_ValueChanged(object sender, bool value)
        {
            _formSetting.openLast = value;
            onFormSave?.Invoke();
        }

        private void swComExcludeFile_ValueChanged(object sender, bool value)
        {
            _formSetting.excludeFile = value;
            onFormSave?.Invoke();
        }

        private void txtComPrefix_Leave(object sender, EventArgs e)
        {
            _formSetting.excludePrefix = txtComPrefix.Text;
            onFormSave?.Invoke();
        }

        private void swJsonCell_ValueChanged(object sender, bool value)
        {
            _jsonSetting.JsonCell = value;
            onJsonSave?.Invoke(false);
        }

        private void swJsonString_ValueChanged(object sender, bool value)
        {
            _jsonSetting.allString = value;
            onJsonSave?.Invoke(false);
        }

        private void swJsonSpace_ValueChanged(object sender, bool value)
        {
            _jsonSetting.saveSpace = value;
            onJsonSave?.Invoke(false);
        }

        private void swJsonMultiFiles_ValueChanged(object sender, bool value)
        {
            _jsonSetting.separateBySheet = value;
            onJsonSave?.Invoke(false);
        }

        private void swJsonExcludeSheet_ValueChanged(object sender, bool value)
        {
            _jsonSetting.excludeSheet = value;
            onJsonSave?.Invoke(false);
        }

        private void swJsonFirstCol_ValueChanged(object sender, bool value)
        {
            _jsonSetting.excludeFirstCol = value;
            onJsonSave?.Invoke(false);
        }

        private void txtJsonDateFormat_Leave(object sender, EventArgs e)
        {
            _jsonSetting.dateFormat = txtJsonDateFormat.Text;
            onJsonSave?.Invoke(false);
        }

        private void txtJsonSavePath_Leave(object sender, EventArgs e)
        {
            _jsonSetting.savePath = txtJsonSavePath.Text;
            onJsonSave?.Invoke(false);
        }

        private void txtJsonExcludePrefix_Leave(object sender, EventArgs e)
        {
            _jsonSetting.excludePrefix = txtJsonSavePath.Text;
            onJsonSave?.Invoke(false);
        }


        private void txtJsonField_Leave(object sender, EventArgs e)
        {
            _jsonSetting.FieldRowNum = int.Parse(txtJsonField.Text) - 1;  //设置里面是索引 外面写的是行号
            onJsonSave?.Invoke(false);
        }

        private void txtJsonStart_Leave(object sender, EventArgs e)
        {
            _jsonSetting.StartRowNum = int.Parse(txtJsonStart.Text) - 1; //设置里面是索引 外面写的是行号
            onJsonSave?.Invoke(false);
        }

        private void swXmlMultiFiles_ValueChanged(object sender, bool value)
        {
            _xmlSetting.separateBySheet = value;
            onXmlSave?.Invoke(false);
        }

        private void swXmlExcludeSheet_ValueChanged(object sender, bool value)
        {
            _xmlSetting.excludeSheet = value;
            onXmlSave?.Invoke(false);
        }

        private void swXmlFirstCol_ValueChanged(object sender, bool value)
        {
            _xmlSetting.excludeFirstCol = value;
            onXmlSave?.Invoke(false);
        }

        private void txtXmlDateFormat_Leave(object sender, EventArgs e)
        {
            _xmlSetting.dateFormat = txtXmlDateFormat.Text;
            onXmlSave?.Invoke(false);
        }

        private void txtXmlSavePath_Leave(object sender, EventArgs e)
        {
            _xmlSetting.savePath = txtXmlSavePath.Text;
            onXmlSave?.Invoke(false);
        }

        private void txtXmlExcludePrefix_Leave(object sender, EventArgs e)
        {
            _xmlSetting.excludePrefix = txtXmlExcludePrefix.Text;
            onXmlSave?.Invoke(false);
        }

        private void txtXmlComment_Leave(object sender, EventArgs e)
        {
            _xmlSetting.CommentRowNum = int.Parse(txtXmlComment.Text) - 1;//设置里面是索引 外面写的是行号
            onXmlSave?.Invoke(false);
        }

        private void txtXmlType_Leave(object sender, EventArgs e)
        {
            _xmlSetting.TypeRowNum = int.Parse(txtXmlType.Text) - 1; //设置里面是索引 外面写的是行号
            onXmlSave?.Invoke(false);
        }

        private void txtXmlField_Leave(object sender, EventArgs e)
        {
            _xmlSetting.FieldRowNum = int.Parse(txtXmlField.Text) - 1; //设置里面是索引 外面写的是行号
            onXmlSave?.Invoke(false);
        }

        private void txtXmlStart_Leave(object sender, EventArgs e)
        {
            _xmlSetting.StartRowNum = int.Parse(txtXmlStart.Text) - 1; //设置里面是索引 外面写的是行号
            onXmlSave?.Invoke(false);
        }

        private void swCsMultiFiles_ValueChanged(object sender, bool value)
        {
            _cSharpSetting.separateBySheet = value;
            onCSharpSave?.Invoke(false);
        }

        private void swCsExcludeSheet_ValueChanged(object sender, bool value)
        {
            _cSharpSetting.excludeSheet = value;
            onCSharpSave?.Invoke(false);
        }

        private void swCsFirstCol_ValueChanged(object sender, bool value)
        {
            _cSharpSetting.excludeFirstCol = value;
            onCSharpSave?.Invoke(false);
        }

        private void txtCsSavePath_Leave(object sender, EventArgs e)
        {
            _cSharpSetting.savePath = txtCsSavePath.Text;
            onCSharpSave?.Invoke(false);
        }

        private void txtCsExcludePrefix_Leave(object sender, EventArgs e)
        {
            _cSharpSetting.excludePrefix = txtCsExcludePrefix.Text;
            onCSharpSave?.Invoke(false);
        }

        private void txtCsComment_Leave(object sender, EventArgs e)
        {
            _cSharpSetting.CommentRowNum = int.Parse(txtCsComment.Text) - 1; //设置里面是索引 外面写的是行号
            onCSharpSave?.Invoke(false);
        }

        private void txtCsField_Leave(object sender, EventArgs e)
        {
            _cSharpSetting.FieldRowNum = int.Parse(txtCsField.Text) - 1; //设置里面是索引 外面写的是行号
            onCSharpSave?.Invoke(false);
        }

        private void txtCsType_Leave(object sender, EventArgs e)
        {
            _cSharpSetting.TypeRowNum = int.Parse(txtCsType.Text) - 1; //设置里面是索引 外面写的是行号
            onCSharpSave?.Invoke(false);
        }

        private void SavePath_DoubleClick(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ((UITextBox)sender).Text = dialog.FileName;
            }
        }

        private void File_DragEnter(object sender, DragEventArgs e)
        {
            string[] dropData = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (dropData != null && dropData.Length > 0 && Directory.Exists(dropData[0]))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void File_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropData = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (dropData == null)
            {
                UIMessageTip.ShowError("你拖入了什么玩意？？？？？");
                return;
            }
            //如果拖入的文件是多个，暂时不做处理直接报错提示
            if (dropData.Length >= 2)
            {
                UIMessageTip.ShowError("请勿移入多个文件");
                return;
            }

            if (Directory.Exists(dropData[0]))
            {
                ((UITextBox)sender).Text = dropData[0];
            }
            else
            {
                UIMessageTip.ShowError("请拖入文件夹");
            }
        }

        private void swExpand_ValueChanged(object sender, bool value)
        {
            _formSetting.isExpand = value;
            onNavMenuChange?.Invoke(value);
            onFormSave?.Invoke();
        }

        private void swCsProperty_ValueChanged(object sender, bool value)
        {
            _cSharpSetting.IsProperty = value;
            onCSharpSave?.Invoke(false);
        }
    }
}
