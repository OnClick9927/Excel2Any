namespace Excel2Any.Winform
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
            this.SuspendLayout();
            // 
            // tabSheets
            // 
            this.tabSheets.Size = new System.Drawing.Size(536, 450);
            this.tabSheets.SelectedIndexChanged += new System.EventHandler(this.tabSheets_SelectedIndexChanged);
            // 
            // TextConvertPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "TextConvertPage";
            this.Text = "TextConvertPage";
            this.ResumeLayout(false);

        }

        #endregion
    }
}