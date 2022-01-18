using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtfUnknown;

namespace ProjectUtf8Process
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        int icount = 0;
        private void BtnConvert_Click(object sender, EventArgs e)
        {
            String folderPath = @txtDirPath.Text.Trim();

            ParseDirectory(@folderPath, (filePath) =>
            {
                // Detect from File (NET standard 1.3+ or .NET 4+)
                DetectionResult result = CharsetDetector.DetectFromFile(filePath); // or pass FileInfo
                
                // Get the best Detection
                DetectionDetail resultDetected = result.Detected;
                if (resultDetected != null) 
                {
                // Get the alias of the found encoding
                string encodingName = resultDetected.EncodingName;

                // Get the System.Text.Encoding of the found encoding (can be null if not available)
                Encoding encoding = resultDetected.Encoding;

                // Get the confidence of the found encoding (between 0 and 1)
                float confidence = resultDetected.Confidence;

                // Get all the details of the result
                IList<DetectionDetail> allDetails = result.Details;

                    ////&& !encoding.ToString().Equals("System.Text.SBCSCodePageEncoding")
                    if (encoding != null && encoding != Encoding.UTF8)
                    {
                        string text = "";
                        using (StreamReader read = new StreamReader(filePath, encoding))
                        {
                            string oldtext = read.ReadToEnd();
                            text = oldtext;
                            text = text.Replace("\n", "\r\n");
                            text = text.Replace("\r\r\n", "\r\n"); // 防止替换了正常的换行符      
                            if (oldtext.Length == text.Length)
                            {
                                labMsg.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1) + " 不需要标准化";
                            }
                        }
                        File.WriteAllText(filePath, text, Encoding.UTF8); //utf-8格式保存，防止乱码

                        icount++;

                        Console.WriteLine(icount.ToString() + ">>>" + filePath.Substring(filePath.LastIndexOf("\\") + 1) + " 行尾标准化完成");

                        labMsg.Text = filePath.Substring(filePath.LastIndexOf("\\") + 1) + " 行尾标准化完成";

                        Application.DoEvents();
                    }
                }
            });
        }



        /// <summary>递归所有的目录，根据过滤器找到文件，并使用委托来统一处理</summary>
        /// <param name="info"></param>
        /// <param name="filter"></param>
        /// <param name="action"></param>
        private void ParseDirectory(string folderPath, Action<string> action)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return;

            labMsg.Text = "读取目录：" + folderPath;
            Application.DoEvents();
            // 处理文件
            // 处理文件
            var fileNameArray = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".cs") || s.EndsWith(".vb")); 
            if (fileNameArray.Count() > 0)
            {
                foreach (var filePath in fileNameArray)
                {
                    action(filePath);
                }
            }
            else
            {
                Console.WriteLine("未发现文件！");
            }
            //得到子目录，递归处理
            string[] dirs = Directory.GetDirectories(folderPath);
            var iter = dirs.GetEnumerator();
            while (iter.MoveNext())
            {
                string str = (string)(iter.Current);
                ParseDirectory(str, action);
            }
        }


        private void BtnChance_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择待转换工程根目录";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "路径不能为空", "提示");
                    return;
                }
                txtDirPath.Text = dialog.SelectedPath;
                BtnConvert.Enabled = true;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            labMsg.Text = "";
        }
    }
}
