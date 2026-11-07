using Lab03_Bai06;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Lab03_Bai06
{
    public partial class FormServer : Form
    {
        private readonly ChatServer server = new ChatServer();

        public FormServer()
        {
            InitializeComponent();

            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            FormClosing += (s, e) => server.Stop();

            server.LogReceived += ShowLog;
            server.OnlineListChanged += ShowOnlineList;

            SetRunningState(false);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(txtIP.Text.Trim(), out IPAddress ip))
            {
                MessageBox.Show("Địa chỉ IP không hợp lệ!");
                return;
            }

            try
            {
                server.Start(ip, (int)numPort.Value);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Không thể mở cổng: " + ex.Message);
                return;
            }

            SetRunningState(true);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
            SetRunningState(false);
        }

        private void SetRunningState(bool running)
        {
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
            txtIP.Enabled = numPort.Enabled = !running;
        }

        private void ShowLog(string line)
        {
            RunOnUI(() =>
            {
                lstLog.Items.Add(line);
                lstLog.TopIndex = lstLog.Items.Count - 1; 
            });
        }

        private void ShowOnlineList(string[] names)
        {
            RunOnUI(() =>
            {
                lstOnline.Items.Clear();
                lstOnline.Items.AddRange(names);
                lblCount.Text = $"Online: {names.Length}";
            });
        }

        private void RunOnUI(Action action)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                try { BeginInvoke(action); }
                catch (InvalidOperationException) { } 
            }
            else
            {
                action();
            }
        }
    }
}

