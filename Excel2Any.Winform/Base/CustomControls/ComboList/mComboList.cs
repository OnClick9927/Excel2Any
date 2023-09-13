using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Sunny.UI;

namespace Excel2Any.Winform
{
    [ToolboxItem(true)]
    public partial class mComboList : UIDropControl
    {
        public mComboList()
        {
            InitializeComponent();
            fullControlSelect = true;
            CreateInstance();
            ButtonClick += OpenPanel;
            item.ValueChanged += (seneder, value) =>
            {
                Text = value.ToString();
            };
        }

        private void OpenPanel(object sender, EventArgs e)
        {
            ItemForm.Show(this);
        }

        private readonly mComboDown item = new mComboDown();

        /// <summary>
        /// 创建对象
        /// </summary>
        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        public void InitItems(List<string> plans, string defaultPlan)
        {
            if (item != null && plans.Count > 0)
            {
                var distinctItems = plans.Distinct();
                item.Clear();
                foreach (var plan in distinctItems)
                {
                    var planItem = item.Add(plan);
                    if (defaultPlan.Equals(planItem.Text))
                    {
                        planItem.Select();
                    }
                }
            }
        }

        public void SubscribeItemDelete(Action<string> action)
        {
            item.onItemDelete += action;
        }
        public void SubscribeItemReName(Action<string, string> action)
        {
            item.onItemReName += action;
        }

        public void SubscribeItemSelect(Action<string> action)
        {
            item.onItemSelect += action;
        }

        public void SubscribeItemAdd(Action action)
        {
            item.onItemAdd += action;
        }

        public void SelectItem(string planName,bool doAction = true)
        {
            item.SelectItem(planName,doAction);
        }
    }
}
