using Microsoft.WindowsAPICodePack.Dialogs;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public partial class SettingPage : UIPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开对应设置
        /// </summary>
        /// <param name="tabName">标签页名</param>
        public void OpenSetting(string tabName)
        {
            for (int i = 0; i < tabSettings.TabPages.Count; i++)
            {
                if (tabName.Equals(tabSettings.TabPages[i].Text.Trim()))
                {
                    tabSettings.SelectTab(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 传入设置属性和转换名，自动生成设置页面
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tabName"></param>
        public void CreateSettingTab(List<FieldInfo> fields, string tabName)
        {
            if (fields.Count == 0) return;
            var tabPage = SettingUIHelper.GetNewTabPage(tabName);
            tabSettings.TabPages.Add(tabPage);
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<SettingAttribute>();
                if (attr == null) continue; //没有的直接跳过
                //生成标题
                var title = SettingUIHelper.GetHeaderLabel(attr.name);
                //生成内容
                if (field.FieldType == typeof(bool))
                {
                    //生成切换按钮
                }
                else if (field.FieldType == typeof(string))
                {

                }

            }

            tabSettings.Refresh();
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

    }
}
