namespace Excel2Other.Winform
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.uiToolTip1 = new Sunny.UI.UIToolTip(this.components);
            this.imglstFiles = new System.Windows.Forms.ImageList(this.components);
            this.tvwFile = new System.Windows.Forms.TreeView();
            this.btnSetting = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // Aside
            // 
            this.Aside.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.Aside.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.Aside.Indent = 23;
            this.Aside.LineColor = System.Drawing.Color.Black;
            this.Aside.Location = new System.Drawing.Point(2, 36);
            this.Aside.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Aside.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.Aside.SelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.Aside.SelectedForeColor = System.Drawing.Color.White;
            this.Aside.SelectedHighColor = System.Drawing.Color.Silver;
            this.Aside.ShowItemsArrow = false;
            this.Aside.ShowNodeToolTips = true;
            this.Aside.ShowPlusMinus = false;
            this.Aside.ShowRootLines = false;
            this.Aside.Size = new System.Drawing.Size(127, 562);
            this.Aside.Style = Sunny.UI.UIStyle.Custom;
            this.Aside.StyleCustomMode = true;
            this.Aside.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.Aside_MenuItemClick);
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.AutoPopDelay = 5000;
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.InitialDelay = 300;
            this.uiToolTip1.OwnerDraw = true;
            this.uiToolTip1.ReshowDelay = 100;
            // 
            // imglstFiles
            // 
            this.imglstFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstFiles.ImageStream")));
            this.imglstFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstFiles.Images.SetKeyName(0, "Folder.png");
            this.imglstFiles.Images.SetKeyName(1, "excel.png");
            // 
            // tvwFile
            // 
            this.tvwFile.AllowDrop = true;
            this.tvwFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.tvwFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvwFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tvwFile.HideSelection = false;
            this.tvwFile.ImageIndex = 1;
            this.tvwFile.ImageList = this.imglstFiles;
            this.tvwFile.Indent = 10;
            this.tvwFile.LineColor = System.Drawing.Color.DimGray;
            this.tvwFile.Location = new System.Drawing.Point(60, 35);
            this.tvwFile.Name = "tvwFile";
            this.tvwFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tvwFile.SelectedImageIndex = 0;
            this.tvwFile.ShowNodeToolTips = true;
            this.tvwFile.Size = new System.Drawing.Size(121, 185);
            this.tvwFile.TabIndex = 2;
            this.tvwFile.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwFile_NodeMouseClick);
            this.tvwFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvwFile_DragDrop);
            this.tvwFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvwFile_DragEnter);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btnSetting.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.btnSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetting.Location = new System.Drawing.Point(5, 548);
            this.btnSetting.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnSetting.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btnSetting.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSetting.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.btnSetting.Size = new System.Drawing.Size(118, 47);
            this.btnSetting.Style = Sunny.UI.UIStyle.Custom;
            this.btnSetting.StyleCustomMode = true;
            this.btnSetting.Symbol = 61459;
            this.btnSetting.TabIndex = 4;
            this.btnSetting.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetting.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.ControlBoxFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.tvwFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(2, 36, 2, 2);
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ShowDragStretch = true;
            this.ShowRadius = false;
            this.ShowTitleIcon = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Text = "Excel转换器";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 799, 659);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Controls.SetChildIndex(this.Aside, 0);
            this.Controls.SetChildIndex(this.tvwFile, 0);
            this.Controls.SetChildIndex(this.btnSetting, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIToolTip uiToolTip1;
        private System.Windows.Forms.ImageList imglstFiles;
        private System.Windows.Forms.TreeView tvwFile;
        private Sunny.UI.UISymbolButton btnSetting;
    }
}