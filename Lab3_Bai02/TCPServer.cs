using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Bai02
{
    public partial class TCPServer : Form
    {
        public TCPServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = true;
            serverThread.Start();

            btnListen.Enabled = false;
            AddLog("Telnet running on 127.0.0.1:8080");
        }

        void StartUnsafeThread()
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(-1);

            while (true)
            {
                // Chờ và đón một Client mới
                Socket clientSocket = listenerSocket.Accept();
                AddLog("New client connected");

                // hàm HandleClient xử lý trên 1 Thread mới
                Thread clientThread = new Thread(() => HandleClient(clientSocket));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
        }

        // Hàm hỗ trợ ghi log an toàn lên UI (tránh InvalidOperationException)
        private void AddLog(string text)
        {
            if (listViewCommand.InvokeRequired)
            {
                listViewCommand.Invoke(new Action(() => {
                    listViewCommand.Items.Add(new ListViewItem(text));
                }));
            }
            else
            {
                listViewCommand.Items.Add(new ListViewItem(text));
            }
        }

        // Hàm phục vụ riêng cho từng Client
        void HandleClient(Socket clientSocket)
        {
            byte[] recv = new byte[1];
            try
            {
                while (clientSocket.Connected)
                {
                    string text = "";
                    int bytesReceived = 0;
                    do
                    {
                        bytesReceived = clientSocket.Receive(recv);
                        if (bytesReceived == 0) break; // Client ngắt kết nối

                        text += Encoding.ASCII.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    if (bytesReceived == 0 || string.IsNullOrWhiteSpace(text)) break;

                    AddLog(text.Trim());
                }
            }
            catch (SocketException)
            {
                // Bắt lỗi khi Client đột ngột đóng cửa sổ CMD
            }
            finally
            {
                clientSocket.Close();
            }
        }

    }
}
