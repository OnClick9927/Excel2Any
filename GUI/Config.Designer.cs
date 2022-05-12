namespace excel2other.GUI
{
    partial class Config
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
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtInPath = new System.Windows.Forms.TextBox();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.checkBoxAllString = new System.Windows.Forms.CheckBox();
            this.checkBoxCellJson = new System.Windows.Forms.CheckBox();
            this.textBoxExculdePrefix = new System.Windows.Forms.TextBox();
            this.comboBoxSheetName = new System.Windows.Forms.ComboBox();
            this.comboBoxDateFormat = new System.Windows.Forms.ComboBox();
            this.comboBoxLowcase = new System.Windows.Forms.ComboBox();
            this.comboBoxHeader = new System.Windows.Forms.ComboBox();
            this.comboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(6, 179);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(102, 12);
            label7.TabIndex = 13;
            label7.Text = "ExculdePrefix:";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(6, 153);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(102, 12);
            label6.TabIndex = 11;
            label6.Text = "SheetName:";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(6, 127);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(102, 12);
            label5.TabIndex = 9;
            label5.Text = "Date Format:";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(6, 75);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(102, 12);
            label4.TabIndex = 6;
            label4.Text = "Lowcase:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(6, 101);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(102, 12);
            label3.TabIndex = 4;
            label3.Text = "Header:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(6, 49);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(102, 12);
            label2.TabIndex = 1;
            label2.Text = "Encoding:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(31, 23);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 12);
            label1.TabIndex = 1;
            label1.Text = "Export Type:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.Location = new System.Drawing.Point(6, 285);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(102, 12);
            label8.TabIndex = 17;
            label8.Text = "输出路径：";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.Location = new System.Drawing.Point(6, 258);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(102, 12);
            label9.TabIndex = 17;
            label9.Text = "输入路径：";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtInPath);
            this.groupBox1.Controls.Add(label9);
            this.groupBox1.Controls.Add(this.txtOutputPath);
            this.groupBox1.Controls.Add(label8);
            this.groupBox1.Controls.Add(this.checkBoxAllString);
            this.groupBox1.Controls.Add(this.checkBoxCellJson);
            this.groupBox1.Controls.Add(this.textBoxExculdePrefix);
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(label6);
            this.groupBox1.Controls.Add(this.comboBoxSheetName);
            this.groupBox1.Controls.Add(label5);
            this.groupBox1.Controls.Add(this.comboBoxDateFormat);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(this.comboBoxLowcase);
            this.groupBox1.Controls.Add(label3);
            this.groupBox1.Controls.Add(this.comboBoxHeader);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.comboBoxEncoding);
            this.groupBox1.Controls.Add(this.comboBoxType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 481);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(177, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(21, 410);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "确认";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtInPath
            // 
            this.txtInPath.Location = new System.Drawing.Point(114, 259);
            this.txtInPath.Name = "txtInPath";
            this.txtInPath.Size = new System.Drawing.Size(150, 21);
            this.txtInPath.TabIndex = 18;
            this.txtInPath.DoubleClick += new System.EventHandler(this.txtInPath_DoubleClick);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(114, 286);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(150, 21);
            this.txtOutputPath.TabIndex = 18;
            this.txtOutputPath.DoubleClick += new System.EventHandler(this.txtOutputPath_DoubleClick);
            // 
            // checkBoxAllString
            // 
            this.checkBoxAllString.AutoSize = true;
            this.checkBoxAllString.Location = new System.Drawing.Point(12, 378);
            this.checkBoxAllString.Name = "checkBoxAllString";
            this.checkBoxAllString.Size = new System.Drawing.Size(84, 16);
            this.checkBoxAllString.TabIndex = 16;
            this.checkBoxAllString.Text = "All String";
            this.checkBoxAllString.UseVisualStyleBackColor = true;
            // 
            // checkBoxCellJson
            // 
            this.checkBoxCellJson.AutoSize = true;
            this.checkBoxCellJson.Location = new System.Drawing.Point(12, 358);
            this.checkBoxCellJson.Name = "checkBoxCellJson";
            this.checkBoxCellJson.Size = new System.Drawing.Size(186, 16);
            this.checkBoxCellJson.TabIndex = 15;
            this.checkBoxCellJson.Text = "Convert Json String in Cell";
            this.checkBoxCellJson.UseVisualStyleBackColor = true;
            // 
            // textBoxExculdePrefix
            // 
            this.textBoxExculdePrefix.Location = new System.Drawing.Point(114, 180);
            this.textBoxExculdePrefix.Name = "textBoxExculdePrefix";
            this.textBoxExculdePrefix.Size = new System.Drawing.Size(150, 21);
            this.textBoxExculdePrefix.TabIndex = 14;
            // 
            // comboBoxSheetName
            // 
            this.comboBoxSheetName.DisplayMember = "0";
            this.comboBoxSheetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSheetName.FormattingEnabled = true;
            this.comboBoxSheetName.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.comboBoxSheetName.Location = new System.Drawing.Point(114, 153);
            this.comboBoxSheetName.Name = "comboBoxSheetName";
            this.comboBoxSheetName.Size = new System.Drawing.Size(150, 20);
            this.comboBoxSheetName.TabIndex = 10;
            this.comboBoxSheetName.ValueMember = "0";
            // 
            // comboBoxDateFormat
            // 
            this.comboBoxDateFormat.DisplayMember = "0";
            this.comboBoxDateFormat.FormattingEnabled = true;
            this.comboBoxDateFormat.Items.AddRange(new object[] {
            "yyyy/MM/dd",
            "yyyy/MM/dd hh:mm:ss"});
            this.comboBoxDateFormat.Location = new System.Drawing.Point(114, 127);
            this.comboBoxDateFormat.Name = "comboBoxDateFormat";
            this.comboBoxDateFormat.Size = new System.Drawing.Size(150, 20);
            this.comboBoxDateFormat.TabIndex = 8;
            this.comboBoxDateFormat.ValueMember = "0";
            // 
            // comboBoxLowcase
            // 
            this.comboBoxLowcase.DisplayMember = "0";
            this.comboBoxLowcase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLowcase.FormattingEnabled = true;
            this.comboBoxLowcase.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.comboBoxLowcase.Location = new System.Drawing.Point(114, 75);
            this.comboBoxLowcase.Name = "comboBoxLowcase";
            this.comboBoxLowcase.Size = new System.Drawing.Size(150, 20);
            this.comboBoxLowcase.TabIndex = 5;
            this.comboBoxLowcase.ValueMember = "0";
            // 
            // comboBoxHeader
            // 
            this.comboBoxHeader.DisplayMember = "0";
            this.comboBoxHeader.FormattingEnabled = true;
            this.comboBoxHeader.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBoxHeader.Location = new System.Drawing.Point(114, 101);
            this.comboBoxHeader.Name = "comboBoxHeader";
            this.comboBoxHeader.Size = new System.Drawing.Size(150, 20);
            this.comboBoxHeader.TabIndex = 3;
            this.comboBoxHeader.ValueMember = "0";
            // 
            // comboBoxEncoding
            // 
            this.comboBoxEncoding.DisplayMember = "0";
            this.comboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoding.FormattingEnabled = true;
            this.comboBoxEncoding.Location = new System.Drawing.Point(114, 49);
            this.comboBoxEncoding.Name = "comboBoxEncoding";
            this.comboBoxEncoding.Size = new System.Drawing.Size(150, 20);
            this.comboBoxEncoding.TabIndex = 0;
            this.comboBoxEncoding.ValueMember = "0";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DisplayMember = "0";
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Array",
            "Dict Object"});
            this.comboBoxType.Location = new System.Drawing.Point(114, 23);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(150, 20);
            this.comboBoxType.TabIndex = 0;
            this.comboBoxType.ValueMember = "0";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(280, 481);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Config";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxAllString;
        private System.Windows.Forms.CheckBox checkBoxCellJson;
        private System.Windows.Forms.TextBox textBoxExculdePrefix;
        private System.Windows.Forms.ComboBox comboBoxSheetName;
        private System.Windows.Forms.ComboBox comboBoxDateFormat;
        private System.Windows.Forms.ComboBox comboBoxLowcase;
        private System.Windows.Forms.ComboBox comboBoxHeader;
        private System.Windows.Forms.ComboBox comboBoxEncoding;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtInPath;
    }
}