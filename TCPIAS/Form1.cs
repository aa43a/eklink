using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPIAS
{
    public partial class Form1 : Form
    {
        TCPias.TCPiaSer tis = new TCPias.TCPiaSer();
        public Form1()
        {
            InitializeComponent();
           
           // tis.StartListener("127.0.0.1",1001);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             // TCPias.OpenVpnn.opencmd();
            Thread th = null,ts = null;
            MessageBox.Show("开始线程?");

            ts = new Thread(TCPias.OpenVpnn.finishOther);
            
            th = new Thread(TCPias.OpenVpnn.opencmd);
            ts.Start();
            th.Start();
            MessageBox.Show("开始线程");
            if (!th.IsAlive)
            {
                tbResult.Text = globa.global.s;
            }
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
             tbResult.Text = "";

             ProcessStartInfo start = new ProcessStartInfo(@"ping.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到

            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe

             start.Arguments = txtCommand.Text;//设置命令参数

            start.CreateNoWindow = false;//不显示dos命令行窗口

            start.RedirectStandardOutput = true;//

            start.RedirectStandardInput = true;//

            start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序

            Process p=Process.Start(start);

            StreamReader reader = p.StandardOutput;//截取输出流

            string line = reader.ReadLine();//每次读取一行

            while (!reader.EndOfStream)

            {

                tbResult.AppendText(line+" ");

                line = reader.ReadLine();

            }

            p.WaitForExit();//等待程序执行完退出进程

            //p.Close();//关闭进程

            reader.Close();//关闭流
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TCPias.OpenVpnn.KillProcess();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = TCPias.OpenVpnn.GetIP();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            globa.global.path = System.Environment.CurrentDirectory;
            Thread th = null,ts = null;
            th = new Thread(TCPias.TapVirInstell.InstallTAPVir);
            ts = new Thread(TCPias.TapVirInstell.InstallTAPVir);
            th.Start();
            ts.Start();
            //th.Start();
            textBox3.Text = globa.global.path;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            globa.global.path = System.Environment.CurrentDirectory;
            Thread th = null;
            th = new Thread(TCPias.TapVirInstell.RemoveTAPVir);
            th.Start();
            textBox3.Text = globa.global.path;
        }
    }
}
