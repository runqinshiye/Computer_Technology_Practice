namespace CRMClient
{
    partial class FileUpLoadForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalSize = new System.Windows.Forms.TextBox();
            this.txtBytesSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PrograssBar = new System.Windows.Forms.ProgressBar();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "总大小:";
            // 
            // txtTotalSize
            // 
            this.txtTotalSize.Location = new System.Drawing.Point(94, 32);
            this.txtTotalSize.Name = "txtTotalSize";
            this.txtTotalSize.Size = new System.Drawing.Size(100, 21);
            this.txtTotalSize.TabIndex = 1;
            // 
            // txtBytesSize
            // 
            this.txtBytesSize.Location = new System.Drawing.Point(333, 37);
            this.txtBytesSize.Name = "txtBytesSize";
            this.txtBytesSize.Size = new System.Drawing.Size(100, 21);
            this.txtBytesSize.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "已上传:";
            // 
            // PrograssBar
            // 
            this.PrograssBar.Location = new System.Drawing.Point(47, 74);
            this.PrograssBar.Name = "PrograssBar";
            this.PrograssBar.Size = new System.Drawing.Size(386, 23);
            this.PrograssBar.TabIndex = 4;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(182, 113);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(107, 34);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "取消上传";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // FileUpLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 168);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.PrograssBar);
            this.Controls.Add(this.txtBytesSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotalSize);
            this.Controls.Add(this.label1);
            this.Name = "FileUpLoadForm";
            this.Text = "文件上传（进度条）";
            this.Load += new System.EventHandler(this.FileUpLoadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalSize;
        private System.Windows.Forms.TextBox txtBytesSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar PrograssBar;
        private System.Windows.Forms.Button btn_Cancel;
    }
}