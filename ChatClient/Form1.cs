using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        StreamReader streamReader;
        StreamWriter streamWriter;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Connect()
        {
            TcpClient tcpClient = new TcpClient();

            tcpClient.Connect("127.0.0.1", 49300);

            NetworkStream networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
            streamWriter.AutoFlush = true;

            //streamWriter.Flush();

            btnSend.Enabled = true;

            while (true)
            {
                string msg = await streamReader.ReadLineAsync();
                txtReceivedMsgs.Text += $"{Environment.NewLine} {msg}";
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine(txtMsgs.Text);

        }
    }
}
