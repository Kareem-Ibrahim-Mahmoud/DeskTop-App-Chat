using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    internal class Client
    {
        public event EventHandler<string> MsgReceived;

        TcpClient tcpClient;
        StreamReader streamReader;
        StreamWriter streamWriter;


        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;

            NetworkStream networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
            streamWriter.AutoFlush = true;

            ReadMsgs();
        }

        public void SendMsg(string msg)
        {
            streamWriter.WriteLine(msg);
        }

        private async void ReadMsgs()
        {
            while (true)
            {
                string msg = await streamReader.ReadLineAsync();

                if(MsgReceived != null)
                    MsgReceived(this, msg);
                
            }
        }
    }
}
