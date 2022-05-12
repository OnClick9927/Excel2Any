namespace excel2other.GUI {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lvwData = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReimport = new System.Windows.Forms.Button();
            this.tabControlCode = new System.Windows.Forms.TabControl();
            this.tabPageJSON = new System.Windows.Forms.TabPage();
            this.btnSaveJSON = new System.Windows.Forms.Button();
            this.btnCopyJSON = new System.Windows.Forms.Button();
            this.tabXML = new System.Windows.Forms.TabPage();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.btnCopyXML = new System.Windows.Forms.Button();
            this.tabCSharp = new System.Windows.Forms.TabPage();
            this.btnSaveCSharp = new System.Windows.Forms.Button();
            this.btnCopyCSharp = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControlCode.SuspendLayout();
            this.tabPageJSON.SuspendLayout();
            this.tabXML.SuspendLayout();
            this.tabCSharp.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImportExcel,
            this.toolStripSeparator1,
            this.btnConfig});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(784, 48);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "Import excel file and export as JSON";
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Image = global::excel2other.Properties.Resources.excel;
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(85, 45);
            this.btnImportExcel.Text = "Import Excel";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.ToolTipText = "Import Excel .xlsx file";
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // btnConfig
            // 
            this.btnConfig.Image = global::excel2other.Properties.Resources.code;
            this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(86, 45);
            this.btnConfig.Text = "Open Config";
            this.btnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlCode);
            this.splitContainer1.Size = new System.Drawing.Size(784, 492);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lvwData);
            this.flowLayoutPanel1.Controls.Add(this.btnReimport);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(286, 490);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lvwData
            // 
            this.lvwData.AllowDrop = true;
            this.lvwData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPath});
            this.lvwData.FullRowSelect = true;
            this.lvwData.GridLines = true;
            this.lvwData.HideSelection = false;
            this.lvwData.Location = new System.Drawing.Point(8, 8);
            this.lvwData.Margin = new System.Windows.Forms.Padding(8);
            this.lvwData.MultiSelect = false;
            this.lvwData.Name = "lvwData";
            this.lvwData.Size = new System.Drawing.Size(270, 442);
            this.lvwData.TabIndex = 2;
            this.lvwData.UseCompatibleStateImageBehavior = false;
            this.lvwData.View = System.Windows.Forms.View.Details;
            this.lvwData.SelectedIndexChanged += new System.EventHandler(this.lvwData_SelectedIndexChanged);
            this.lvwData.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwData_DragDrop);
            this.lvwData.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwData_DragEnter);
            // 
            // colName
            // 
            this.colName.Text = "文件名";
            // 
            // colPath
            // 
            this.colPath.Text = "文件路径";
            this.colPath.Width = 199;
            // 
            // btnReimport
            // 
            this.btnReimport.Location = new System.Drawing.Point(3, 461);
            this.btnReimport.Name = "btnReimport";
            this.btnReimport.Size = new System.Drawing.Size(264, 24);
            this.btnReimport.TabIndex = 7;
            this.btnReimport.Text = "重新读取文件夹";
            this.btnReimport.UseVisualStyleBackColor = true;
            this.btnReimport.Click += new System.EventHandler(this.btnReimport_Click);
            // 
            // tabControlCode
            // 
            this.tabControlCode.Controls.Add(this.tabPageJSON);
            this.tabControlCode.Controls.Add(this.tabXML);
            this.tabControlCode.Controls.Add(this.tabCSharp);
            this.tabControlCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCode.Location = new System.Drawing.Point(0, 0);
            this.tabControlCode.Name = "tabControlCode";
            this.tabControlCode.SelectedIndex = 0;
            this.tabControlCode.Size = new System.Drawing.Size(490, 490);
            this.tabControlCode.TabIndex = 0;
            // 
            // tabPageJSON
            // 
            this.tabPageJSON.Controls.Add(this.btnSaveJSON);
            this.tabPageJSON.Controls.Add(this.btnCopyJSON);
            this.tabPageJSON.Location = new System.Drawing.Point(4, 22);
            this.tabPageJSON.Name = "tabPageJSON";
            this.tabPageJSON.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJSON.Size = new System.Drawing.Size(482, 464);
            this.tabPageJSON.TabIndex = 0;
            this.tabPageJSON.Text = "JSON";
            this.tabPageJSON.UseVisualStyleBackColor = true;
            // 
            // btnSaveJSON
            // 
            this.btnSaveJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveJSON.Location = new System.Drawing.Point(398, 6);
            this.btnSaveJSON.Name = "btnSaveJSON";
            this.btnSaveJSON.Size = new System.Drawing.Size(75, 23);
            this.btnSaveJSON.TabIndex = 1;
            this.btnSaveJSON.Text = "保存Json";
            this.btnSaveJSON.UseVisualStyleBackColor = true;
            this.btnSaveJSON.Click += new System.EventHandler(this.btnSaveJSON_Click);
            // 
            // btnCopyJSON
            // 
            this.btnCopyJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyJSON.Location = new System.Drawing.Point(317, 6);
            this.btnCopyJSON.Name = "btnCopyJSON";
            this.btnCopyJSON.Size = new System.Drawing.Size(75, 23);
            this.btnCopyJSON.TabIndex = 0;
            this.btnCopyJSON.Text = "复制Json";
            this.btnCopyJSON.UseVisualStyleBackColor = true;
            this.btnCopyJSON.Click += new System.EventHandler(this.btnCopyJSON_Click);
            // 
            // tabXML
            // 
            this.tabXML.Controls.Add(this.btnSaveXML);
            this.tabXML.Controls.Add(this.btnCopyXML);
            this.tabXML.Location = new System.Drawing.Point(4, 22);
            this.tabXML.Name = "tabXML";
            this.tabXML.Padding = new System.Windows.Forms.Padding(3);
            this.tabXML.Size = new System.Drawing.Size(482, 464);
            this.tabXML.TabIndex = 2;
            this.tabXML.Text = "XML";
            this.tabXML.UseVisualStyleBackColor = true;
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveXML.Location = new System.Drawing.Point(400, 6);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(75, 23);
            this.btnSaveXML.TabIndex = 3;
            this.btnSaveXML.Text = "保存XML";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // btnCopyXML
            // 
            this.btnCopyXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyXML.Location = new System.Drawing.Point(319, 6);
            this.btnCopyXML.Name = "btnCopyXML";
            this.btnCopyXML.Size = new System.Drawing.Size(75, 23);
            this.btnCopyXML.TabIndex = 2;
            this.btnCopyXML.Text = "复制XML";
            this.btnCopyXML.UseVisualStyleBackColor = true;
            this.btnCopyXML.Click += new System.EventHandler(this.btnCopyXML_Click);
            // 
            // tabCSharp
            // 
            this.tabCSharp.Controls.Add(this.btnSaveCSharp);
            this.tabCSharp.Controls.Add(this.btnCopyCSharp);
            this.tabCSharp.Location = new System.Drawing.Point(4, 22);
            this.tabCSharp.Name = "tabCSharp";
            this.tabCSharp.Padding = new System.Windows.Forms.Padding(3);
            this.tabCSharp.Size = new System.Drawing.Size(482, 464);
            this.tabCSharp.TabIndex = 1;
            this.tabCSharp.Text = "C#";
            this.tabCSharp.UseVisualStyleBackColor = true;
            // 
            // btnSaveCSharp
            // 
            this.btnSaveCSharp.Location = new System.Drawing.Point(400, 6);
            this.btnSaveCSharp.Name = "btnSaveCSharp";
            this.btnSaveCSharp.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCSharp.TabIndex = 3;
            this.btnSaveCSharp.Text = "保存C#";
            this.btnSaveCSharp.UseVisualStyleBackColor = true;
            this.btnSaveCSharp.Click += new System.EventHandler(this.btnSaveCSharp_Click);
            // 
            // btnCopyCSharp
            // 
            this.btnCopyCSharp.Location = new System.Drawing.Point(319, 6);
            this.btnCopyCSharp.Name = "btnCopyCSharp";
            this.btnCopyCSharp.Size = new System.Drawing.Size(75, 23);
            this.btnCopyCSharp.TabIndex = 2;
            this.btnCopyCSharp.Text = "复制C#";
            this.btnCopyCSharp.UseVisualStyleBackColor = true;
            this.btnCopyCSharp.Click += new System.EventHandler(this.btnCopyCSharp_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "Ready";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusLabel.Size = new System.Drawing.Size(108, 17);
            this.statusLabel.Text = "叶子三分青 修改版";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "excel转换程序";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControlCode.ResumeLayout(false);
            this.tabPageJSON.ResumeLayout(false);
            this.tabXML.ResumeLayout(false);
            this.tabCSharp.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControlCode;
        private System.Windows.Forms.TabPage tabPageJSON;
        private System.Windows.Forms.TabPage tabCSharp;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button btnSaveJSON;
        private System.Windows.Forms.Button btnCopyJSON;
        private System.Windows.Forms.Button btnSaveCSharp;
        private System.Windows.Forms.Button btnCopyCSharp;
        private System.Windows.Forms.Button btnReimport;
        private System.Windows.Forms.TabPage tabXML;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Button btnCopyXML;
        private System.Windows.Forms.ListView lvwData;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ToolStripButton btnConfig;
    }
}