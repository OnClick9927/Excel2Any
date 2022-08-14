using Sunny.UI;
using System;
using System.Drawing;

namespace Excel2Any.Winform
{
    public partial class mComboDownItem : UIPanel
    {
        private string nowName;
        public mComboDown parent;
        public new string Text => nowName;

        public void SetName(string newName)
        {
            newName = newName.Trim();
            if (nowName.Equals(newName)) return;
            //SettingHelper.ReNamePlan(nowName, newName);
            parent.onItemReName?.Invoke(nowName, newName);
            nowName = newName;
            txtPlan.Text = newName;
            txtPlan.Enabled = false;
        }

        public mComboDownItem(string plan, bool even = false)
        {
            InitializeComponent();
            nowName = plan;
            txtPlan.Text = plan;

            if (even)
            {
                this.fillColor = Color.Black;
            }
        }

        private void btnReName_Click(object sender, EventArgs e)
        {
            StartEdit();
        }

        public void StartEdit()
        {
            Editing();
            txtPlan.Focus();

        }
        public void EndEdit()
        {
            if (string.IsNullOrWhiteSpace(txtPlan.Text) && !string.IsNullOrEmpty(nowName))
            {
                txtPlan.Text = nowName;
                UIMessageTip.ShowError("名称不能为空！");
            }
            else if (parent.Contains(txtPlan.Text))
            {
                txtPlan.Text = nowName;
                UIMessageTip.ShowError("名称与原先一致或含有同名的配置！");
            }
            else
            {
                SetName(txtPlan.Text);
                UIMessageTip.ShowOk("修改成功！");
            }

            DefaultState();
        }

        public void CancelEdit()
        {
            if (txtPlan.Enabled)
            {
                txtPlan.Text = nowName;
                DefaultState();
            }
            
        }
        private void txtPlan_DoEnter(object sender, EventArgs e)
        {
            EndEdit();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            Select();
        }

        public new void Select()
        {
            if (!string.IsNullOrEmpty(nowName))
            {
                parent.SelectItem(this);
                Selected();
            }
            else
            {
                UIMessageTip.ShowError("请先命名");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EndEdit();
        }

        public void SetFocus()
        {
            fillColor = Color.Green;
        }

        public void NotFocus()
        {
            fillColor = Color.White;
        }


        #region 状态
        //三个状态  正在编辑 编辑完成 选择
        public void Editing()
        {
            txtPlan.Enabled = true;
            btnOK.Visible = true;
            btnReName.Visible = false;
            btnSelect.Visible = false;
            lblSelect.Visible = false;
        }

        public void DefaultState()
        {
            txtPlan.Enabled = false;
            btnOK.Visible = false;
            btnReName.Visible = true;
            btnSelect.Visible = true;
            lblSelect.Visible = false;
        }

        public void Selected() 
        {
            txtPlan.Enabled = false;
            btnOK.Visible = false;
            btnReName.Visible = false;
            btnSelect.Visible = false;
            lblSelect.Visible = true;
        }
        #endregion

        private void SetFocus(object sender, EventArgs e)
        {
            if (!txtPlan.Enabled)
            {
                parent.SetCurrentItem(this);
            }
        }
    }
}
