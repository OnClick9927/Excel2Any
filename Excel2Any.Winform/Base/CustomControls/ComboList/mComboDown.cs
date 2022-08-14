using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;

namespace Excel2Any.Winform
{
    public partial class mComboDown : UIDropDownItem
    {
        public Action<string> onItemDelete;
        public Action<string, string> onItemReName;
        public Action<string> onItemSelect;

        public mComboDownItem Current;
        public mComboDownItem SelectedItem;
        public mComboDown()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            Add("");
        }

        public void Clear()
        {
            var controls = new List<mComboDownItem>();
            foreach (var item in pnlContainer.Controls)
            {
                if (item is mComboDownItem)
                {
                    controls.Add((mComboDownItem)item);
                }
            }

            foreach (var control in controls)
            {
                pnlContainer.Controls.Remove(control);
                control.Dispose();
            }
        }

        public mComboDownItem Add(string name)
        {
            var item = new mComboDownItem(name);
            item.parent = this;
            pnlContainer.Add(item);
            item.StartEdit();
            return item;
        }

        public List<string> GetList()
        {
            List<string> plans = new List<string>();
            foreach (var item in pnlContainer.Controls)
            {
                if (item is mComboDownItem)
                {
                    var text = ((mComboDownItem)item).ToString();
                    plans.Add(text);
                }
            }
            return plans;
        }

        public bool Contains(string plan)
        {
            foreach (var item in pnlContainer.FlowLayoutPanel.Controls)
            {
                if (item is mComboDownItem)
                {
                    var text = ((mComboDownItem)item).Text;
                    if (!string.IsNullOrEmpty(text) && text.Equals(plan))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void DeleteItem()
        {
            if (Current != null)
            {
                pnlContainer.Controls.Remove(Current);
                if (!string.IsNullOrEmpty(Current.Text))
                {
                    onItemDelete?.Invoke(Current.Text);
                }
                Current.Dispose();
                Current = null;
                UIMessageTip.Show("已删除!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (UIMessageDialog.ShowAskDialog(UIForm.ActiveForm, "是否要删除这个配置",UIStyle.Black))
            {
                DeleteItem();
            }            
        }

        public void SelectItem(mComboDownItem select)
        {
            foreach (var item in pnlContainer.FlowLayoutPanel.Controls)
            {
                if (item is mComboDownItem)
                {
                    (item as mComboDownItem).DefaultState();
                }
            }
            select.Selected();
            DoValueChanged(this, select.Text);
            SelectedItem = select;
            onItemSelect?.Invoke(select.Text);
        }

        public void SetCurrentItem(mComboDownItem current)
        {
            foreach (var item in pnlContainer.FlowLayoutPanel.Controls)
            {
                if (item is mComboDownItem)
                {
                    (item as mComboDownItem).NotFocus();
                }
            }
            current.SetFocus();
            Current = current;
        }
    }
}
