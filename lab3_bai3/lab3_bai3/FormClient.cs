using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace lab3_bai3
{
    public partial class FormClient : Form
    {
        private TcpClient? tcpClient;
        private NetworkStream? ns;
        public FormClient()
        {
            InitializeComponent();
            btnSend.Enabled = false;
            btnDisconnect.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient();
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8080);

                tcpClient.Connect(ipEndPoint);
                ns = tcpClient.GetStream();

                MessageBox.Show("Kết nối thành công đến Server!");
                btnConnect.Enabled = false;
                btnSend.Enabled = true;
                btnDisconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kết nối thất bại: {ex.Message}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient != null && tcpClient.Connected && ns != null)
            {
                string message = txtMessage.Text + "\n";
                byte[] data = Encoding.UTF8.GetBytes(message);

                ns.Write(data, 0, data.Length);
                ns.Flush();

                txtMessage.Clear();
            }
            else
            {
                MessageBox.Show("Chưa kết nối đến Server.");
            }
        }

        

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }
        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
        private void Disconnect()
        {
            try
            {
                if (ns != null)
                {
                    byte[] data = Encoding.UTF8.GetBytes("quit\n");
                    ns.Write(data, 0, data.Length);
                    ns.Close();
                    ns = null;
                }

                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                }

                btnConnect.Enabled = true;
                btnSend.Enabled = false;
                btnDisconnect.Enabled = false;
            }
            catch (Exception)
            {
                
            }
        }
    }
}
