using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRMClient.Service;
using System.IO;
using System.ServiceModel;

namespace CRMClient
{
    public partial class FileUpLoadForm : Form
    {
        FileModel fileModel ;

        FileServiceClient fileServiceClient;//服务端和客户端的对接对象

        BackgroundWorker bw = new BackgroundWorker();
        public FileUpLoadForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public FileUpLoadForm(string filePath)
        {
            InitializeComponent();
            //文件信息
            FileInfo fileInfo = new FileInfo(filePath);
            fileModel = new FileModel();
            fileModel.FileId = Guid.NewGuid().ToString();//生成36位唯一标识
            fileModel.FileName = fileInfo.Name;
            fileModel.FileExtName = fileInfo.Extension;
            fileModel.FileSize = fileInfo.Length;
            fileModel.FullName = fileInfo.FullName;
            //实现回调
            FileServiceCallBack fileServiceCallBack=new FileServiceCallBack();
            //注册回调
            fileServiceCallBack.ToFileSizeCallBack += fileServiceCallBack_ToFileSizeCallBack;
            //客户端和服务端进行对接（调用服务端的方法）,FileServiceClient其实就是服务端的 FileService，这里系统自动加了个Client后缀
            fileServiceClient = new FileServiceClient(new InstanceContext(fileServiceCallBack));
           //调用服务端的方法，上传文件
            fileServiceClient.FileUpLoad(fileModel);
           
        }

        /// <summary>
        /// 修改进度条信息（通知回调）
        /// </summary>
        /// <param name="positionSize">文件大小</param>
        void fileServiceCallBack_ToFileSizeCallBack(long positionSize)
        {
            //步长，进度条走一步相当于多少个字节
            int stepSize = (int)(this.fileModel.FileSize / this.PrograssBar.Maximum);
            //未传输完
            if (positionSize > stepSize * PrograssBar.Value)
            {
                //进度条走了多少步
                int result = this.PrograssBar.Value + 1;
                long SizeMb = positionSize / 1024 / 1024;//传输了几MB
                //如果线程未结束
                if (this.bw.IsBusy)
                {
                    //报告进度条进度
                    this.bw.ReportProgress(result, SizeMb);//手动触发bw_ProgressChanged方法
                }
            }
        }

        private void FileUpLoadForm_Load(object sender, EventArgs e)
        {
            this.txtTotalSize.Text = (fileModel.FileSize / 1024 / 1024).ToString()+"MB";//将字节b单位转换为MB
            this.PrograssBar.Minimum = 0;
            this.PrograssBar.Maximum = 100;
            this.bw.WorkerReportsProgress = true;//报告进度条的更新，不设置可能导致bw_ProgressChanged不触发    
            this.bw.WorkerSupportsCancellation = true;//是否支持取消线程
            //多线程控件，三个方法
            bw.DoWork += bw_DoWork;//开启线程触发
            bw.ProgressChanged+=bw_ProgressChanged;//辅助线程执行的时候触发
            bw.RunWorkerCompleted+=bw_RunWorkerCompleted;//任务结束的时候触发
            //开始任务,执行bw_DoWork
            this.bw.RunWorkerAsync();
        }

        public event Action<FileModel> CloseFormCallBack;

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(this.PrograssBar.Value>=this.PrograssBar.Maximum)
            {
                this.Close();//关闭
                //并通知文件上传窗体已经成功上传（回调）
                CloseFormCallBack(fileModel);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.PrograssBar.Value = e.ProgressPercentage > 100 ? 100 : e.ProgressPercentage;//显示进度条百分比
            if (e.UserState!=null)
           {
               this.txtBytesSize.Text = e.UserState.ToString();//实时文件大小
           }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            using(FileStream fileStream = new FileStream(fileModel.FullName,FileMode.Open))//打开文件
           {
                //当文件还未传完
                while(fileStream.Position<fileStream.Length)
                {
                    //如果终止上传
                    if (this.bw.CancellationPending)
                    {
                        this.bw.ReportProgress(0, null);//设置进度条为0
                        e.Cancel = true;//取消事件
                        return;
                    }
                    //循环上传，每次10kb
                    byte[] bytes=new byte[10240];
                    //循环读取文件内容,返回每次读取的实际大小
                   int count= fileStream.Read(bytes,0,bytes.Length);
                   fileModel.FileBytes = bytes;
                   fileModel.FileCount = count;
                   fileServiceClient.FileTransfer(fileModel);//循环发送
                }
           }
        }
        //取消上传
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.bw.CancelAsync();//取消后，bw.CancellationPending=true
            this.fileServiceClient.FileStop(this.fileModel);
            this.Close();
        }
    }
}
