using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPIAS.TCPias
{
    class OpenVpnn
    {
        public void StartLink(string path)
        {
            Process.Start(path);
        }
    
        public static void openvpnToServer()
        {
           
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";

            cmd.StartInfo.UseShellExecute = false;      
   
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;   
            cmd.StartInfo.RedirectStandardError = false;
            cmd.StartInfo.CreateNoWindow = true;          

            cmd.Start();
           // cmd.StandardInput.WriteLine("g:\ncd G:\\Program Files\\OpenVPN\\config\nweb.ovpn\ntest3\ntest3\n");

            cmd.StandardInput.WriteLine("c:");

            cmd.StandardInput.WriteLine(@"cd " + globa.global.path + @"\openvpn");
           
            cmd.StandardInput.WriteLine("openvpn --config web.ovpn --auth-user-pass ss.txt");
            
            StreamReader reader = cmd.StandardOutput;
            string line = reader.ReadLine();//每次读取一行
           
            while (!reader.EndOfStream)
            {

                Console.WriteLine(reader.ReadLine());
                globa.global.s += line;
                line = reader.ReadLine();
                

            }

            reader.Close();
            cmd.StandardInput.Flush();

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            cmd.Close();
        }

        public static void openvpnSetRoute()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";

            cmd.StartInfo.UseShellExecute = false;

            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = false;
            cmd.StartInfo.CreateNoWindow = true;

            cmd.Start();
            // cmd.StandardInput.WriteLine("g:\ncd G:\\Program Files\\OpenVPN\\config\nweb.ovpn\ntest3\ntest3\n");

            cmd.StandardInput.WriteLine("c:");

            cmd.StandardInput.WriteLine(@"cd " + globa.global.path + @"\openvpn");

            cmd.StandardInput.WriteLine("openvpn --config route.ovpn");

            StreamReader reader = cmd.StandardOutput;
            string line = reader.ReadLine();//每次读取一行

            while (!reader.EndOfStream)
            {

                Console.WriteLine(reader.ReadLine());
                globa.global.s += line;
                line = reader.ReadLine();


            }

            reader.Close();
            cmd.StandardInput.Flush();

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            cmd.Close();
        }

        public static void SetRouteIP(string ip) {
            string strline;
            List<string> srw = new List<string>();
           // ip = "10.8.0.49";
            FileStream fs = new FileStream(globa.global.path + @"\openvpn\route.ovpn", FileMode.Open);
            //FileStream fs = new FileStream(@"C: \Users\zhangsf\Desktop\OpenVPN\config\route1.ovpn", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            StreamWriter sw = new StreamWriter(fs);
            strline = sr.ReadLine();
            
            while (strline != null) {
                Console.WriteLine(strline);
                
                if (strline.StartsWith("remote"))
                {
                    srw.Add("remote " + ip + " 10088");
                }
                else {
                    srw.Add(strline);
                }
                strline = sr.ReadLine();                
            }
            fs.SetLength(0);
            foreach (string sinw in srw) {
                sw.WriteLine(sinw);
            }
            sw.Close();
            sr.Close();
            fs.Close();
        }

        public static void finishOther(){ 
             int con = 0;
             Process pro = new Process();
           
             while (globa.global.statrvpn == false)
             {
                
                 foreach (Process p in Process.GetProcesses())
                 {
                     if (p.ProcessName == "openvpn")
                     {
                         con++;
                         globa.global.statrvpn = true;
                     }
                 }
                 Console.WriteLine(globa.global.statrvpn+"  hello.");
                     foreach (Process p in Process.GetProcesses())
                     {
                    
                         if (con != 0)
                         {
                             //Console.WriteLine(p.ProcessName);
                             if (p.ProcessName == "conhost")
                             {
                                 p.Kill();
                                 con++;
                             }
                             if (p.ProcessName == "cmd")
                             {
                                 p.Kill();
                                 con++;
                             }
                         }
                     }
                     Thread.Sleep(500);
                 }
        }

        public static void KillProcess() {
            Process pro = new Process();
            foreach (Process p in Process.GetProcesses()) {
            //    Console.WriteLine(p.ProcessName);
                if (p.ProcessName == "openvpn") {
                    p.Kill();
                }
            }
            globa.global.statrvpn = false;
            finishOther();
        }

        public static string GetIP(){

            System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            for (int i = 0; i != IpEntry.AddressList.Length; i++)
            {
                if (IpEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "IP获取失败";

            //System.Net.IPHostEntry ipHost = System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            //System.Net.IPAddress ipAddr = ipHost.AddressList[0];
            //return ipAddr.ToString();
        }

        public static string showMessage() {
            return globa.global.s;
        }
    }
}
