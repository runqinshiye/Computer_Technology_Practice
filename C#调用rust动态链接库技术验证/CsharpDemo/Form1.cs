using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("rustdll.dll",EntryPoint = "output_int", CallingConvention = CallingConvention.Cdecl)]
        //下面把 output_int 改成符合 C# 命名习惯的用法，不改就是 int output_int() 
        private static extern int OutputInt(int n);
        static int TestOutputInt(int n)
        {
            return OutputInt(n);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int o = TestOutputInt((int)numericUpDown1.Value);
            textBox1.Text = o.ToString();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

    }
}
