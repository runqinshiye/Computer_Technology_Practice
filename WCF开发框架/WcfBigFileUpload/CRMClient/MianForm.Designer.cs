namespace CRMClient
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bnt_UpLoad = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btn_Browser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnt_UpLoad
            // 
            this.bnt_UpLoad.Location = new System.Drawing.Point(239, 162);
            this.bnt_UpLoad.Name = "bnt_UpLoad";
            this.bnt_UpLoad.Size = new System.Drawing.Size(138, 37);
            this.bnt_UpLoad.TabIndex = 0;
            this.bnt_UpLoad.Text = "上传";
            this.bnt_UpLoad.UseVisualStyleBackColor = true;
            this.bnt_UpLoad.Click += new System.EventHandler(this.bnt_UpLoad_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(186, 66);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(260, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // btn_Browser
            // 
            this.btn_Browser.Location = new System.Drawing.Point(468, 66);
            this.btn_Browser.Name = "btn_Browser";
            this.btn_Browser.Size = new System.Drawing.Size(77, 21);
            this.btn_Browser.TabIndex = 2;
            this.btn_Browser.Text = "浏览";
            this.btn_Browser.UseVisualStyleBackColor = true;
            this.btn_Browser.Click += new System.EventHandler(this.btn_Browser_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 264);
            this.Controls.Add(this.btn_Browser);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.bnt_UpLoad);
            this.Name = "MainForm";
            this.Text = "文件上传";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnt_UpLoad;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btn_Browser;
    }
}

