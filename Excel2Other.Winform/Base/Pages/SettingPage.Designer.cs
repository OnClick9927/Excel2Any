namespace Excel2Other.Winform
{
    partial class SettingPage
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
            this.tabSettings = new Sunny.UI.UITabControlMenu();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSettings.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabSettings.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tabSettings.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabSettings.ItemSize = new System.Drawing.Size(40, 130);
            this.tabSettings.Location = new System.Drawing.Point(0, 0);
            this.tabSettings.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.tabSettings.Multiline = true;
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(800, 450);
            this.tabSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSettings.Style = Sunny.UI.UIStyle.Custom;
            this.tabSettings.StyleCustomMode = true;
            this.tabSettings.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.tabSettings.TabIndex = 0;
            this.tabSettings.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.tabSettings.TabSelectedForeColor = System.Drawing.Color.White;
            this.tabSettings.TabSelectedHighColor = System.Drawing.Color.Silver;
            this.tabSettings.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.tabSettings.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // SettingPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabSettings);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Name = "SettingPage";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITabControlMenu tabSettings;
    }
}