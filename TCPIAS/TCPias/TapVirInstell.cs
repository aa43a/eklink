using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPIAS.TCPias
{
    class TapVirInstell
    {

        public static void InstallTAPVir()
        {
            Process cmd = new Process();
            StartProcess(cmd);
            cmd.StandardInput.WriteLine("c:");
            cmd.StandardInput.WriteLine(@"cd " + globa.global.path + @"\openvpn\driver");
            cmd.StandardInput.WriteLine("devcon -r install OemWin2k.inf tap0901");
            StreamReader reader = cmd.StandardOutput;
            string line = reader.ReadLine();//每次读取一行

            while (!reader.EndOfStream)
            {

                Console.WriteLine(reader.ReadLine());
                globa.global.s += line;
                line = reader.ReadLine();


            }
            EndProcess(cmd);
        }

        public static void RemoveTAPVir()
        {
            Process cmd = new Process();
            StartProcess(cmd);
            cmd.StandardInput.WriteLine("c:");
            cmd.StandardInput.WriteLine(@"cd " + globa.global.path + @"\openvpn\driver");
            cmd.StandardInput.WriteLine("devcon /r remove tap0901");
            EndProcess(cmd);
        }


        public static void StartProcess(Process cmd) {
            cmd.StartInfo.FileName = "cmd.exe";

            cmd.StartInfo.UseShellExecute = false;

            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = false;
            cmd.StartInfo.CreateNoWindow = true;

            cmd.Start();
        }

        public static void EndProcess(Process cmd) {
            cmd.StandardInput.Flush();

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            cmd.Close();
        }
    }
}
