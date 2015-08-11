using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCPIAS.TCPias
{
    class TCPiaSer
    {
        private TcpListener listener;
        private Socket socket,client;
        private byte[] buffer = new byte[1024];
        public TCPiaSer() { 
        
        }

        public TCPiaSer(Socket socket,Socket client){
            socket = this.socket;
            client =this.client;
        }

        //public void StartListener(string ip,int port) {
        //    socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
        //    listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
        //    listener.Start(); 
        //    listener.BeginAcceptSocket(clientConnect, listener);
            
        //}

        private void clientConnect(IAsyncResult ar) {
            try
            {
                listener = (TcpListener)ar.AsyncState;
                client = listener.EndAcceptSocket(ar);
            }
            catch (Exception e) {
                throw e;
            }
        }

        private void receiveData()
        {
            int BagSize = 1024;
            try
            {
                IAsyncResult iar = client.BeginReceive(buffer, 0, BagSize, SocketFlags.None, receiveCallback, buffer);
            }
            catch (Exception e) {
                throw e;
            }
        }

        private void receiveCallback(IAsyncResult ar) {
            int receLen = 0;           
            try
            {
                receLen = client.EndReceive(ar);

                if (receLen > 0) {
                    OnReceiveData(client);
                }
            }
            catch (Exception e) {
                throw e;
            }
        }

        private void OnReceiveData(Socket socket) {
            string strLogin = "succeed recived";
            socket = this.socket;
            byte[] data = Encoding.ASCII.GetBytes(strLogin);

            socket.BeginSend(data, 0, data.Length, SocketFlags.None, sendCallback, socket);
        }

        private void sendCallback(IAsyncResult ar) {
            socket.EndSend(ar);
        }
        public void Funcc(ref int i){i = 3;
        //    if(!socket.Connected()){
        

        //}
        }
    }
}
