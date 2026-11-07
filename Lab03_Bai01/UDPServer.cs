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

namespace Lab03
{
    public partial class UDPServer : Form
    {
        public UDPServer()
        {
            InitializeComponent();
        }

        private void UDPServer_Load(object sender, EventArgs e)
        {
            Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
            thdUDPServer.IsBackground = true; // Cho phép tắt thread khi đóng form
            thdUDPServer.Start();
        }

        public void serverThread()
        {
            UdpClient udpClient = null;
            try
            {
                udpClient = new UdpClient(8080);
                while (true)
                {
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    string mess = RemoteIpEndPoint.Address.ToString() + ": " + returnData;

                    InfoMessage(mess);
                }
            }
            catch (SocketException ex)
            {
                // Lỗi này xảy ra khi Port 8080 đã có phần mềm khác (hoặc chính form này mở 2 lần) chiếm dụng
                MessageBox.Show("Cổng 8080 đang bị chiếm dụng. Vui lòng tắt các tiến trình mạng khác hoặc khởi động lại ứng dụng.\nLỗi: " + ex.Message,
                                "Lỗi Socket", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Server: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (udpClient != null)
                {
                    udpClient.Close();
                }
            }
        }

        private void InfoMessage(string mess)
        {
            // Kiểm tra xem có cần dùng Invoke để tránh lỗi cross-thread không
            if (lsvMessages.InvokeRequired)
            {
                lsvMessages.Invoke(new Action(() => {
                    lsvMessages.Items.Add(new ListViewItem(mess));
                }));
            }
            else
            {
                lsvMessages.Items.Add(new ListViewItem(mess));
            }
        }

    }
}
