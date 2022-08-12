﻿using Sunny.UI;
using System;
using System.Drawing;
using System.Reflection;

namespace Excel2Other.Winform
{
    public partial class SettingPage : UIPage
    {
        private bool saveChange = true;
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
        public void CreateSettingTab(ISetting setting, string tabName, Type entityType = null)
        {
            var settingType = setting.GetType();
            var fields = SettingHelper.GetFields(setting.GetType());
            if (fields.Count == 0) return;
            fields.RemoveAll((f) => { return f.GetCustomAttribute<SettingAttribute>() == null; });
            fields.Sort((x, y) =>
            {
                int xPriority = x.GetCustomAttribute<SettingAttribute>().priority;
                int yPriority = y.GetCustomAttribute<SettingAttribute>().priority;
                return xPriority - yPriority;
            });
            var tabPage = SettingUIHelper.GetTabPage(tabName);
            //留存Type 方便反射获取设置
            tabPage.entityType = entityType;
            tabSettings.TabPages.Add(tabPage);

            var panel = SettingUIHelper.GetPanel();

            int panelLocation = 0;
            #region 上方按钮部分
            if (!tabName.Equals("通用"))
            {
                var panelContainer = SettingUIHelper.GetUIPanel();
                //生成按钮
                var loadButton = SettingUIHelper.GetUIButton("设置读取");
                var saveButton = SettingUIHelper.GetUIButton("设置另存为");
                panelContainer.Controls.Add(loadButton);
                panelContainer.Controls.Add(saveButton);

                //按钮绑定方法
                loadButton.Click += (sender, e) =>
                {
                    SettingHelper.LoadSetting(entityType, true);
                    RefreshUI(tabPage);
                };
                saveButton.Click += (sender, e) =>
                {
                    SettingHelper.SaveSetting(entityType, true);
                };

                loadButton.Location = new Point(30, 10);
                saveButton.Location = new Point(150, 10);

                panel.Add(panelContainer);

                panelContainer.Location = new Point(0, panelLocation);
                panelLocation = panelContainer.Location.Y + panelContainer.Size.Height + 20;

                panel.Invalidate();
            }
            #endregion

            #region 内容部分
            foreach (var field in fields)
            {
                var panelContainer = SettingUIHelper.GetUIPanel();

                int location = 10;
                var attr = field.GetCustomAttribute<SettingAttribute>();
                //生成标题
                var title = SettingUIHelper.GetHeaderLabel(attr.name + $" ({field.FieldType.Name})");
                var content = SettingUIHelper.GetContentLabel(attr.des);
                //生成内容
                //分为两种  一个是切换按钮的  一个是填框框的，框框的文本框类型需要考虑
                panelContainer.Controls.Add(title);
                title.Location = new Point(30, location);
                location += 30;

                if (field.FieldType == typeof(bool))
                {
                    //生成切换按钮
                    var uiSwitch = SettingUIHelper.GetSwith();
                    panelContainer.Controls.Add(uiSwitch);
                    panelContainer.Controls.Add(content);
                    uiSwitch.Location = new Point(30, location);
                    content.Location = new Point(110, location + 4);
                    location += 30;
                    uiSwitch.Active = (bool)field.GetValue(setting);
                    uiSwitch.Name = field.Name;
                    uiSwitch.ActiveChanged += (sender, e) =>
                    {
                        field.SetValue(setting, uiSwitch.Active);
                        if (saveChange)
                        {
                            SaveAndRefreshSetting(setting, entityType);
                        }
                    };
                }
                else
                {
                    UITextBox inputBox = null;
                    if (field.FieldType == typeof(string))
                    {
                        inputBox = SettingUIHelper.GetInputBox(attr.textType);

                        inputBox.Text = field.GetValue(setting).ToString();
                        inputBox.Leave += (sender, e) =>
                        {
                            field.SetValue(setting, inputBox.Text); 
                            SaveAndRefreshSetting(setting, entityType);
                        };
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        //int用于行号，所以索引需要-1  后续看情况更改
                        inputBox = SettingUIHelper.GetInputBox(StringType.Integer);
                        inputBox.Text = ((int)field.GetValue(setting) + 1).ToString();
                        inputBox.Leave += (sender, e) =>
                        {
                            int.TryParse(inputBox.Text, out int num);
                            field.SetValue(setting, num - 1);
                            SaveAndRefreshSetting(setting, entityType);
                        };
                    }
                    inputBox.Name = field.Name;
                    panelContainer.Controls.Add(inputBox);
                    panelContainer.Controls.Add(content);

                    content.Location = new Point(30, location);
                    inputBox.Location = new Point(30, location + 30);
                    location += 60;
                }

                panel.Add(panelContainer);

                panelContainer.Location = new Point(0, panelLocation);
                panelLocation = panelContainer.Location.Y + panelContainer.Size.Height + 20;
            }
            #endregion
            var blankLabel = SettingUIHelper.GetUILabel();
            panel.Add(blankLabel);
            blankLabel.Location = new Point(0, panelLocation);

            tabPage.Controls.Add(panel);
            tabPage.panel = panel;
            tabSettings.Invalidate();
        }

        private void SaveAndRefreshSetting(ISetting setting, Type entityType)
        {
            SettingHelper.SaveSetting(setting);
            if (entityType != null)
            {
                ExcelHelper.GetEntity(entityType).SetSetting(setting);
                ExcelHelper.SetAllDirty(entityType);
            }
        }

        public void RefreshUI(Type entityType)
        {
            for (int i = 0; i < tabSettings.TabPages.Count; i++)
            {
                var page = (mPage)tabSettings.TabPages[i];
                if (page.entityType == entityType)
                {
                    RefreshUI(page);
                    break;
                }
            }
        }
        public void RefreshUI()
        {
            foreach (var item in tabSettings.TabPages)
            {
                var page = (mPage)item;
                RefreshUI(page);
            }
        }
        public void RefreshUI(mPage page)
        {
            //防止修改时多次调用保存设置
            saveChange = false;
            if (page.entityType == null) return;

            ISetting setting = UIEntityHelper.GetUIEntity(page.entityType).setting;

            foreach (var control in page.panel.GetAllControl())
            {
                if (control.GetType() == typeof(UISwitch))
                {
                    var mSwitch = (UISwitch)control;
                    var field = setting.GetType().GetField(mSwitch.Name);
                    mSwitch.Active = (bool)field.GetValue(setting);
                }
                else if (control.GetType() == typeof(UITextBox))
                {
                    var textBox = (UITextBox)control;
                    var field = setting.GetType().GetField(textBox.Name);
                    if (field.FieldType == typeof(int))
                    {
                        textBox.Text = ((int)field.GetValue(setting) + 1).ToString();
                    }
                    else
                    {
                        textBox.Text = field.GetValue(setting).ToString();
                    }
                }
            }

            saveChange = true;
        }

    }
}
