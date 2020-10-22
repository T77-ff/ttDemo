using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Enquire
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IPEndPoint iep;
        Socket sock;
        private void button1_Click(object sender, EventArgs e)
        {
            if(sock == null)
            {
                iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(iep);
            }
            sock.Send(Encoding.GetEncoding("utf-8").GetBytes(this.textBox1.Text));
            Thread thread = new Thread(AcceptResult);
            thread.Start(sock);
        }
        private void AcceptResult(object sock)
        {
            Socket s = sock as Socket;
            while (true)
            {
                int n = s.Available;
                if (n > 0)
                {
                    byte[] data = new byte[n];
                    s.Receive(data, SocketFlags.None);
                    string result = Encoding.GetEncoding("utf-8").GetString(data);
                    MessageBox.Show(result);
                    string[] items = result.Split('\t');
                    this.lblBookId.Text = items[0];
                    this.lblBookName.Text = items[1];
                    this.lblBookType.Text = items[2];
                    this.lblBookAuthor.Text = items[3];
                    this.lblBookPrice.Text = items[4];
                }
                Thread.Sleep(300);
            }
        }
    }
}
