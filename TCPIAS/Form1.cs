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
            textBox5.Text = "10.8.0.51";
            MessConnect.MemGet.StartMemCa();
            checklink();
            //Console.WriteLine(TCPias.GetIP.IsRightIP("10.8.0.6"));
            // tis.StartListener("127.0.0.1",1001);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            globa.global.s = "";
            // globa.global.path = System.Environment.CurrentDirectory;
            // TCPias.OpenVpnn.openvpnToServer();
            Thread th = null,ts = null;
            MessageBox.Show("开始线程?");

            ts = new Thread(TCPias.OpenVpnn.finishOther);
            
            th = new Thread(TCPias.OpenVpnn.openvpnToServer);
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

            start.CreateNoWindow = true;//不显示dos命令行窗口

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
            //
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          //  globa.global.path = System.Environment.CurrentDirectory;
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
         //   globa.global.path = System.Environment.CurrentDirectory;
            Thread th = null;
            th = new Thread(TCPias.TapVirInstell.RemoveTAPVir);
            th.Start();
            textBox2.Text = globa.global.path;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            globa.global.s = "";
            if (!globa.global.statrvpn)
            {
              //  MessageBox.Show("openvpn未连接");
                Thread ts = new Thread(TCPias.OpenVpnn.openvpnToServer);
                Thread th = new Thread(TCPias.OpenVpnn.openvpnSetRoute);
                ts.Start();
                globa.global.statrvpn = true;
                th.Start();
                textBox2.Text = globa.global.s;
            }
            else {
                Thread th = new Thread(TCPias.OpenVpnn.openvpnSetRoute);
                th.Start();
                textBox4.Text = globa.global.s;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TCPias.OpenVpnn.SetRouteIP(textBox5.Text);
        }      

        private void timer1_Tick(object sender, EventArgs e)
        {        
                textBox3.Text = TCPias.OpenVpnn.showMessage();     
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessConnect.MemGet.MemcacheSet(textBox6.Text,textBox7.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox8.Text = MessConnect.MemGet.MemcacheGet(textBox9.Text);
        }
        private int count = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            checklink();
        }

        private void checklink() {
            foreach (Process p in Process.GetProcesses())
            {

                if (p.ProcessName == "openvpn")
                {
                    count++;

                }
            }
            if (count == 1)
            {
                textBox1.Text = "openvpn已连接至115";
            }
            else if (count == 2)
            {
                textBox1.Text = "openvpn已连接至115\n route 已连接至route";
            }
            else
            {
                textBox1.Text = "未连接";
            }
            count = 0;
        }
    }
}
