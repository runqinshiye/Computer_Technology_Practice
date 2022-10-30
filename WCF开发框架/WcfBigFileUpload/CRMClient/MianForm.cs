using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Browser_Click(object sender, EventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            if(sfd.ShowDialog()==DialogResult.OK)
            {
                txtFilePath.Text = sfd.FileName;
            }
        }

        private void bnt_UpLoad_Click(object sender, EventArgs e)
        {
          FileUpLoadForm fileUpLoadForm = new FileUpLoadForm(txtFilePath.Text);
            //注册回调
          fileUpLoadForm.CloseFormCallBack += fileUpLoadForm_CloseFormCallBack;
          fileUpLoadForm.Show();
        }

        void fileUpLoadForm_CloseFormCallBack(Service.FileModel obj)
        {
            MessageBox.Show(obj.FileName+"已上传完毕");
        }

        
    }
}
