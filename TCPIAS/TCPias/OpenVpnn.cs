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
    
        public static void opencmd()
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
           

            cmd.StandardInput.Flush();

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            cmd.Close();
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
                 Console.WriteLine(globa.global.statrvpn);
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
        }

        public static string GetIP(){

            //System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            //for (int i = 0; i != IpEntry.AddressList.Length; i++)
            //{
            //    if (IpEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        return IpEntry.AddressList[i].ToString();
            //    }
            //}
            //return "IP获取失败";

            System.Net.IPHostEntry ipHost = System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            System.Net.IPAddress ipAddr = ipHost.AddressList[0];
            return ipAddr.ToString();
        }
    }
}
