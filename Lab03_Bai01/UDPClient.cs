using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class UDPClient : Form
    {
        public UDPClient()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào không được để trống
            if (string.IsNullOrWhiteSpace(txtIP.Text) || string.IsNullOrWhiteSpace(txtPort.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ IP và Port của Server!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung tin nhắn!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UdpClient udpClient = new UdpClient();
                byte[] sendBytes = Encoding.UTF8.GetBytes(txtMessage.Text);

                string ip = txtIP.Text.Trim();
                int port = int.Parse(txtPort.Text.Trim());

                udpClient.Send(sendBytes, sendBytes.Length, ip, port);
                txtMessage.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Port phải là một con số (ví dụ: 8080).", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi tin nhắn. Chi tiết lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
