namespace Excel2Other.Winform
{
    partial class TextConvertPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextConvertPage));
            this.txtCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            this.MenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            this.uiContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSheets
            // 
            this.tabSheets.Size = new System.Drawing.Size(536, 450);
            this.tabSheets.SelectedIndexChanged += new System.EventHandler(this.tabSheets_SelectedIndexChanged);
            // 
            // txtCode
            // 
            this.txtCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtCode.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.txtCode.BackBrush = null;
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtCode.CharCnWidth = 17;
            this.txtCode.CharHeight = 14;
            this.txtCode.CharWidth = 8;
            this.txtCode.ContextMenuStrip = this.uiContextMenuStrip1;
            this.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.txtCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCode.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtCode.IsReplaceMode = false;
            this.txtCode.LineNumberColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(133)))));
            this.txtCode.Location = new System.Drawing.Point(50, 50);
            this.txtCode.Name = "txtCode";
            this.txtCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.txtCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCode.ServiceColors")));
            this.txtCode.ServiceLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCode.Size = new System.Drawing.Size(150, 150);
            this.txtCode.TabIndex = 2;
            this.txtCode.Text = "fastColoredTextBox1";
            this.txtCode.Zoom = 100;
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiContextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiContextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiContextMenuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemCopy,
            this.MenuItemCopyAll});
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip1.ShowImageMargin = false;
            this.uiContextMenuStrip1.ShowItemToolTips = false;
            this.uiContextMenuStrip1.Size = new System.Drawing.Size(120, 56);
            this.uiContextMenuStrip1.Style = Sunny.UI.UIStyle.Custom;
            this.uiContextMenuStrip1.StyleCustomMode = true;
            this.uiContextMenuStrip1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // MenuItemCopy
            // 
            this.MenuItemCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MenuItemCopy.Name = "MenuItemCopy";
            this.MenuItemCopy.Size = new System.Drawing.Size(155, 26);
            this.MenuItemCopy.Text = "复制选中";
            this.MenuItemCopy.Click += new System.EventHandler(this.MenuItemCopy_Click);
            // 
            // MenuItemCopyAll
            // 
            this.MenuItemCopyAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MenuItemCopyAll.Name = "MenuItemCopyAll";
            this.MenuItemCopyAll.Size = new System.Drawing.Size(155, 26);
            this.MenuItemCopyAll.Text = "复制所有";
            this.MenuItemCopyAll.Click += new System.EventHandler(this.MenuItemCopyAll_Click);
            // 
            // TextConvertPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "TextConvertPage";
            this.Text = "TextConvertPage";
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            this.uiContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected FastColoredTextBoxNS.FastColoredTextBox txtCode;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCopyAll;
    }
}