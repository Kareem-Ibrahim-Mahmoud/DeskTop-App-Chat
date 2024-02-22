using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {

        List<Client> clients;

        public Form1()
        {
            InitializeComponent();

            clients = new List<Client>();
        }

        private async void Start()
        {
            //IPAddress ip = new IPAddress(new byte[] { 192, 168, 1, 12 });
            //IPAddress ip = IPAddress.Parse("192.168.1.12");
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            //IPAddress ip = IPAddress.Parse("localhost");

            TcpListener tcpListener = new TcpListener(ip, 49300);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                Client client = new Client(tcpClient);
                client.MsgReceived += Client_MsgReceived;
                clients.Add(client);

                btnSend.Enabled = true;
            }
            
        }

        private void Client_MsgReceived(object sender, string msg)
        {
            txtReceivedMsgs.Text += $"{Environment.NewLine} {msg}";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            foreach (Client client in clients)
            {
                client.SendMsg(txtMsgs.Text);
            }
        }
    }
}
