namespace Excel2Any.Winform
{
    partial class mComboDownItem
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPlan = new Sunny.UI.UITextBox();
            this.btnReName = new Sunny.UI.UIButton();
            this.btnSelect = new Sunny.UI.UIButton();
            this.lblSelect = new Sunny.UI.UILabel();
            this.btnOK = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // txtPlan
            // 
            this.txtPlan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPlan.Enabled = false;
            this.txtPlan.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPlan.Location = new System.Drawing.Point(4, 5);
            this.txtPlan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPlan.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ShowText = false;
            this.txtPlan.Size = new System.Drawing.Size(496, 29);
            this.txtPlan.TabIndex = 0;
            this.txtPlan.Text = "一二三四五六七八九十一二三四五六七八九十一二三四五六七八九十";
            this.txtPlan.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPlan.Watermark = "";
            this.txtPlan.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtPlan.DoEnter += new System.EventHandler(this.txtPlan_DoEnter);
            // 
            // btnReName
            // 
            this.btnReName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReName.Location = new System.Drawing.Point(511, 5);
            this.btnReName.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnReName.Name = "btnReName";
            this.btnReName.Size = new System.Drawing.Size(63, 29);
            this.btnReName.TabIndex = 1;
            this.btnReName.Text = "重命名";
            this.btnReName.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnReName.Click += new System.EventHandler(this.btnReName_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(580, 5);
            this.btnSelect.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(59, 29);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选择";
            this.btnSelect.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSelect.Location = new System.Drawing.Point(507, 5);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(106, 29);
            this.lblSelect.TabIndex = 3;
            this.lblSelect.Text = "已选择";
            this.lblSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelect.Visible = false;
            this.lblSelect.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(511, 5);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(63, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Visible = false;
            this.btnOK.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // mComboDownItem
            // 
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnReName);
            this.Controls.Add(this.txtPlan);
            this.Controls.Add(this.lblSelect);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "mComboDownItem";
            this.Size = new System.Drawing.Size(652, 41);
            this.Click += new System.EventHandler(this.SetFocus);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox txtPlan;
        private Sunny.UI.UIButton btnReName;
        private Sunny.UI.UIButton btnSelect;
        private Sunny.UI.UILabel lblSelect;
        private Sunny.UI.UIButton btnOK;
    }
}
