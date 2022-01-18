namespace ProjectUtf8Process
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtDirPath = new System.Windows.Forms.TextBox();
            this.BtnConvert = new System.Windows.Forms.Button();
            this.labMsg = new System.Windows.Forms.Label();
            this.BtnChance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择待转换工程根目录";
            // 
            // txtDirPath
            // 
            this.txtDirPath.Location = new System.Drawing.Point(154, 49);
            this.txtDirPath.Name = "txtDirPath";
            this.txtDirPath.Size = new System.Drawing.Size(504, 21);
            this.txtDirPath.TabIndex = 1;
            this.txtDirPath.Text = "D:\\MaiHe\\GitServer\\openDental20.3";
            // 
            // BtnConvert
            // 
            this.BtnConvert.Location = new System.Drawing.Point(227, 98);
            this.BtnConvert.Name = "BtnConvert";
            this.BtnConvert.Size = new System.Drawing.Size(287, 60);
            this.BtnConvert.TabIndex = 2;
            this.BtnConvert.Text = "转换";
            this.BtnConvert.UseVisualStyleBackColor = true;
            this.BtnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // labMsg
            // 
            this.labMsg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labMsg.Location = new System.Drawing.Point(25, 167);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(714, 45);
            this.labMsg.TabIndex = 3;
            this.labMsg.Text = "提示信息……";
            // 
            // BtnChance
            // 
            this.BtnChance.Location = new System.Drawing.Point(664, 47);
            this.BtnChance.Name = "BtnChance";
            this.BtnChance.Size = new System.Drawing.Size(75, 23);
            this.BtnChance.TabIndex = 4;
            this.BtnChance.Text = "选择";
            this.BtnChance.UseVisualStyleBackColor = true;
            this.BtnChance.Click += new System.EventHandler(this.BtnChance_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 221);
            this.Controls.Add(this.BtnChance);
            this.Controls.Add(this.labMsg);
            this.Controls.Add(this.BtnConvert);
            this.Controls.Add(this.txtDirPath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "转换工程源码为UTF8编码";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDirPath;
        private System.Windows.Forms.Button BtnConvert;
        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.Button BtnChance;
    }
}

