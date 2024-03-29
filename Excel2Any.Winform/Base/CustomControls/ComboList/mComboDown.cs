﻿using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Controls;

namespace Excel2Any.Winform
{
    public partial class mComboDown : UIDropDownItem
    {
        public Action<string> onItemDelete;
        public Action<string, string> onItemReName;
        public Action<string> onItemSelect;
        public Action onItemAdd;

        public mComboDownItem Current;
        public mComboDownItem SelectedItem;
        public mComboDown()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            Add("", true);
            onItemAdd.Invoke();
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

        public mComboDownItem Add(string name, bool rename = false)
        {
            var item = new mComboDownItem(name);
            item.parent = this;
            pnlContainer.Add(item);
            if (rename)
            {
                item.StartEdit();
            }
            onItemAdd?.Invoke();
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
                UIMessageTip.ShowOk("已删除！");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Current != null)
            {
                if (Current.CanDelete)
                {
                    if (UIMessageDialog.ShowAskDialog(null, $"是否要删除{Current.Text}这个配置？", UIStyle.Black))
                    {
                        DeleteItem();
                    }
                }
                else
                {
                    UIMessageTip.ShowWarning("无法删除使用中的配置！");
                }
                
            }
            else
            {
                UIMessageTip.ShowWarning("请选择需要删除的项目！");
            }

        }

        public void SelectItem(mComboDownItem select,bool doAction = true)
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
            if (doAction)
            {
                onItemSelect?.Invoke(select.Text);
            }
           
        }

        public void SelectItem(string planName, bool doAction = true)
        {
            foreach (var item in pnlContainer.FlowLayoutPanel.Controls)
            {
                if (item is mComboDownItem)
                {
                    var text = ((mComboDownItem)item).Text;
                    if (!string.IsNullOrEmpty(text) && text.Equals(planName))
                    {
                        SelectItem((mComboDownItem)item,doAction);
                        break;
                    }
                }
            }
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
