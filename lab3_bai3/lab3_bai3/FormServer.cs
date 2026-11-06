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
    public partial class FormServer : Form
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private bool isListening = false;
        public FormServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            listenThread = new Thread(new ThreadStart(StartUnsafeServer));
            listenThread.IsBackground = true;
            listenThread.Start();

            btnListen.Enabled = false;
            rtbServerLog.AppendText("Server started!\n");
        }
        private void StartUnsafeServer()
        {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
                tcpListener = new TcpListener(ipep);
                tcpListener.Start();
                isListening = true;

                TcpClient client = tcpListener.AcceptTcpClient();
                IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                rtbServerLog.AppendText($"Connection accepted from {clientEndPoint.Address}:{clientEndPoint.Port}\n");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    rtbServerLog.AppendText($"From client: {message}");
                }

                client.Close();
                tcpListener.Stop();
            }
            catch (Exception ex)
            {
                if (isListening)
                {
                    rtbServerLog.AppendText($"Lỗi Server: {ex.Message}\n");
                }
            }
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            if (tcpListener != null)
                tcpListener.Stop();
            if (listenThread != null && listenThread.IsAlive)
                listenThread.Abort();
        }
    }
}
