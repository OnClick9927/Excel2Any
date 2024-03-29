﻿namespace Excel2Any.Winform
{
    partial class BaseConvertPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uiText = new Sunny.UI.UIPanel();
            this.tabSheets = new Sunny.UI.UITabControl();
            this.btnSetting = new Sunny.UI.UISymbolButton();
            this.btnRefresh = new Sunny.UI.UISymbolButton();
            this.btnOpen = new Sunny.UI.UISymbolButton();
            this.btnSave = new Sunny.UI.UISymbolButton();
            this.pnlFiles = new Sunny.UI.UIPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlOperations = new Sunny.UI.UIPanel();
            this.uiToolTip1 = new Sunny.UI.UIToolTip(this.components);
            this.uiText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlOperations.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiText
            // 
            this.uiText.Controls.Add(this.tabSheets);
            this.uiText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiText.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiText.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiText.Location = new System.Drawing.Point(0, 0);
            this.uiText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiText.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiText.Name = "uiText";
            this.uiText.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.uiText.Size = new System.Drawing.Size(536, 400);
            this.uiText.Style = Sunny.UI.UIStyle.Custom;
            this.uiText.TabIndex = 3;
            this.uiText.Text = null;
            this.uiText.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabSheets
            // 
            this.tabSheets.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSheets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSheets.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabSheets.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tabSheets.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabSheets.ItemSize = new System.Drawing.Size(110, 30);
            this.tabSheets.Location = new System.Drawing.Point(0, 0);
            this.tabSheets.MainPage = "";
            this.tabSheets.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.tabSheets.Multiline = true;
            this.tabSheets.Name = "tabSheets";
            this.tabSheets.SelectedIndex = 0;
            this.tabSheets.Size = new System.Drawing.Size(536, 400);
            this.tabSheets.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSheets.Style = Sunny.UI.UIStyle.Custom;
            this.tabSheets.StyleCustomMode = true;
            this.tabSheets.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.tabSheets.TabIndex = 6;
            this.tabSheets.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tabSheets.TabSelectedForeColor = System.Drawing.Color.White;
            this.tabSheets.TabSelectedHighColor = System.Drawing.Color.White;
            this.tabSheets.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabSheets.SelectedIndexChanged += new System.EventHandler(this.tabSheets_SelectedIndexChanged);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSetting.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSetting.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnSetting.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSetting.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetting.Location = new System.Drawing.Point(100, 12);
            this.btnSetting.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnSetting.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnSetting.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnSetting.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnSetting.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnSetting.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.btnSetting.ShowTips = true;
            this.btnSetting.Size = new System.Drawing.Size(38, 35);
            this.btnSetting.Style = Sunny.UI.UIStyle.Custom;
            this.btnSetting.StyleCustomMode = true;
            this.btnSetting.Symbol = 61459;
            this.btnSetting.TabIndex = 4;
            this.btnSetting.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiToolTip1.SetToolTip(this.btnSetting, "转到设置");
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnRefresh.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnRefresh.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnRefresh.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRefresh.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(56, 12);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnRefresh.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnRefresh.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnRefresh.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnRefresh.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnRefresh.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.btnRefresh.ShowTips = true;
            this.btnRefresh.Size = new System.Drawing.Size(38, 35);
            this.btnRefresh.Style = Sunny.UI.UIStyle.Custom;
            this.btnRefresh.StyleCustomMode = true;
            this.btnRefresh.Symbol = 61473;
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiToolTip1.SetToolTip(this.btnRefresh, "刷新目录");
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnOpen.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnOpen.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnOpen.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOpen.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOpen.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnOpen.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnOpen.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnOpen.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnOpen.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnOpen.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.btnOpen.ShowTips = true;
            this.btnOpen.Size = new System.Drawing.Size(38, 35);
            this.btnOpen.Style = Sunny.UI.UIStyle.Custom;
            this.btnOpen.StyleCustomMode = true;
            this.btnOpen.Symbol = 61717;
            this.btnOpen.TabIndex = 1;
            this.btnOpen.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiToolTip1.SetToolTip(this.btnOpen, "打开文件夹");
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSave.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSave.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnSave.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(144, 12);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            this.btnSave.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(203)))), ((int)(((byte)(189)))));
            this.btnSave.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnSave.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(138)))));
            this.btnSave.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.btnSave.ShowTips = true;
            this.btnSave.Size = new System.Drawing.Size(38, 35);
            this.btnSave.Style = Sunny.UI.UIStyle.Custom;
            this.btnSave.StyleCustomMode = true;
            this.btnSave.Symbol = 61639;
            this.btnSave.TabIndex = 2;
            this.btnSave.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiToolTip1.SetToolTip(this.btnSave, "保存所有文件");
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlFiles
            // 
            this.pnlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFiles.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlFiles.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlFiles.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlFiles.ForeColor = System.Drawing.Color.White;
            this.pnlFiles.Location = new System.Drawing.Point(0, 55);
            this.pnlFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlFiles.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlFiles.Name = "pnlFiles";
            this.pnlFiles.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.pnlFiles.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlFiles.Size = new System.Drawing.Size(261, 345);
            this.pnlFiles.Style = Sunny.UI.UIStyle.Custom;
            this.pnlFiles.StyleCustomMode = true;
            this.pnlFiles.TabIndex = 5;
            this.pnlFiles.Text = "文件列表";
            this.pnlFiles.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlFiles);
            this.splitContainer1.Panel1.Controls.Add(this.pnlOperations);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiText);
            this.splitContainer1.Panel2MinSize = 400;
            this.splitContainer1.Size = new System.Drawing.Size(800, 400);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
            // 
            // pnlOperations
            // 
            this.pnlOperations.Controls.Add(this.btnSetting);
            this.pnlOperations.Controls.Add(this.btnRefresh);
            this.pnlOperations.Controls.Add(this.btnOpen);
            this.pnlOperations.Controls.Add(this.btnSave);
            this.pnlOperations.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOperations.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlOperations.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlOperations.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlOperations.Location = new System.Drawing.Point(0, 0);
            this.pnlOperations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlOperations.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlOperations.Name = "pnlOperations";
            this.pnlOperations.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.pnlOperations.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlOperations.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.pnlOperations.Size = new System.Drawing.Size(261, 55);
            this.pnlOperations.Style = Sunny.UI.UIStyle.Custom;
            this.pnlOperations.TabIndex = 0;
            this.pnlOperations.Text = null;
            this.pnlOperations.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.AutoPopDelay = 5000;
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.InitialDelay = 100;
            this.uiToolTip1.OwnerDraw = true;
            this.uiToolTip1.ReshowDelay = 100;
            // 
            // BaseConvertPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "BaseConvertPage";
            this.RectColor = System.Drawing.Color.White;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Text = "";
            this.uiText.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlOperations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel uiText;
        private Sunny.UI.UIPanel pnlFiles;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Sunny.UI.UISymbolButton btnSetting;
        private Sunny.UI.UISymbolButton btnRefresh;
        private Sunny.UI.UISymbolButton btnOpen;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UIToolTip uiToolTip1;
        private Sunny.UI.UIPanel pnlOperations;
        protected internal Sunny.UI.UITabControl tabSheets;
    }
}