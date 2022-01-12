namespace WinFormClient
{
    partial class FormMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHttpPara = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBtnDB = new System.Windows.Forms.RadioButton();
            this.radBtnServer = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "调用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "http-post调用有参方法";
            // 
            // txtHttpPara
            // 
            this.txtHttpPara.Location = new System.Drawing.Point(162, 87);
            this.txtHttpPara.Name = "txtHttpPara";
            this.txtHttpPara.Size = new System.Drawing.Size(120, 21);
            this.txtHttpPara.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBtnServer);
            this.groupBox1.Controls.Add(this.radBtnDB);
            this.groupBox1.Location = new System.Drawing.Point(27, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 69);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库连接模式";
            // 
            // radBtnDB
            // 
            this.radBtnDB.AutoSize = true;
            this.radBtnDB.Checked = true;
            this.radBtnDB.Location = new System.Drawing.Point(48, 33);
            this.radBtnDB.Name = "radBtnDB";
            this.radBtnDB.Size = new System.Drawing.Size(107, 16);
            this.radBtnDB.TabIndex = 0;
            this.radBtnDB.TabStop = true;
            this.radBtnDB.Text = "数据库直连模式";
            this.radBtnDB.UseVisualStyleBackColor = true;
            // 
            // radBtnServer
            // 
            this.radBtnServer.AutoSize = true;
            this.radBtnServer.Location = new System.Drawing.Point(202, 33);
            this.radBtnServer.Name = "radBtnServer";
            this.radBtnServer.Size = new System.Drawing.Size(107, 16);
            this.radBtnServer.TabIndex = 0;
            this.radBtnServer.TabStop = true;
            this.radBtnServer.Text = "中间层服务模式";
            this.radBtnServer.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(75, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(261, 73);
            this.button2.TabIndex = 4;
            this.button2.Text = "功能测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 244);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtHttpPara);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebService服务框架测试客户端";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHttpPara;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radBtnServer;
        private System.Windows.Forms.RadioButton radBtnDB;
        private System.Windows.Forms.Button button2;
    }
}

