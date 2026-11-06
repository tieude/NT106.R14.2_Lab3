using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
   
    public class Bai5Client : Form
    {
        private TextBox txtIp, txtPort, txtName, txtMonAn;
        private Button btnConnect, btnDisconnect, btnThem, btnXoa, btnNgauNhien;
        private RadioButton rbCuaToi, rbCongDong;
        private ListView lvMonAn;
        private Label lblKetQua, lblTrangThai;

        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread receiveThread;
        private bool isConnected;

        public Bai5Client()
        {
            BuildUi();
            SetConnectedState(false);
        }

        private void BuildUi()
        {
            Text = "Bai5 - Client Hôm nay ăn gì?";
            ClientSize = new Size(700, 520);
            StartPosition = FormStartPosition.CenterScreen;

          
            AddLabel("Server IP:", 15, 19);
            txtIp = AddTextBox("127.0.0.1", 85, 15, 110);
            AddLabel("Port:", 205, 19);
            txtPort = AddTextBox("8080", 245, 15, 55);
            AddLabel("Tên:", 315, 19);
            txtName = AddTextBox("", 350, 15, 120);

            btnConnect = AddButton("Connect", 485, 13, 95, 27);
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect = AddButton("Disconnect", 585, 13, 100, 27);
            btnDisconnect.Click += BtnDisconnect_Click;


            AddLabel("Món ăn:", 15, 62);
            txtMonAn = AddTextBox("", 75, 58, 360);
            txtMonAn.KeyDown += TxtMonAn_KeyDown;
            btnThem = AddButton("Thêm món", 445, 56, 110, 27);
            btnThem.Click += BtnThem_Click;
            btnXoa = AddButton("Xóa món chọn", 565, 56, 120, 27);
            btnXoa.Click += BtnXoa_Click;

            rbCuaToi = new RadioButton();
            rbCuaToi.Text = "Món của tôi";
            rbCuaToi.Checked = true;
            rbCuaToi.Location = new Point(15, 98);
            rbCuaToi.AutoSize = true;
            rbCuaToi.CheckedChanged += Scope_CheckedChanged;
            Controls.Add(rbCuaToi);

            rbCongDong = new RadioButton();
            rbCongDong.Text = "Món của cộng đồng";
            rbCongDong.Location = new Point(150, 98);
            rbCongDong.AutoSize = true;
            rbCongDong.CheckedChanged += Scope_CheckedChanged;
            Controls.Add(rbCongDong);

            lvMonAn = new ListView();
            lvMonAn.View = View.Details;
            lvMonAn.FullRowSelect = true;
            lvMonAn.GridLines = true;
            lvMonAn.Columns.Add("STT", 50);
            lvMonAn.Columns.Add("Tên món ăn", 400);
            lvMonAn.Columns.Add("Người thêm", 200);
            lvMonAn.Location = new Point(15, 125);
            lvMonAn.Size = new Size(670, 270);
            lvMonAn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lvMonAn);

        
            btnNgauNhien = AddButton("Hôm nay ăn gì?", 15, 410, 170, 38);
            btnNgauNhien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNgauNhien.Click += BtnNgauNhien_Click;

            lblKetQua = new Label();
            lblKetQua.Text = "";
            lblKetQua.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblKetQua.ForeColor = Color.DarkGreen;
            lblKetQua.Location = new Point(200, 410);
            lblKetQua.Size = new Size(485, 38);
            lblKetQua.TextAlign = ContentAlignment.MiddleLeft;
            lblKetQua.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lblKetQua);

            lblTrangThai = new Label();
            lblTrangThai.Text = "Chưa kết nối.";
            lblTrangThai.Location = new Point(15, 480);
            lblTrangThai.AutoSize = true;
            lblTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(lblTrangThai);

            FormClosing += Bai5Client_FormClosing;
        }


        private void AddLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            Controls.Add(label);
        }

        private TextBox AddTextBox(string text, int x, int y, int width)
        {
            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 23);
            Controls.Add(textBox);
            return textBox;
        }

        private Button AddButton(string text, int x, int y, int width, int height)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            Controls.Add(button);
            return button;
        }

   

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            {
                MessageBox.Show("Port không hợp lệ.");
                return;
            }
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Hãy nhập tên của bạn.");
                return;
            }

            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(txtIp.Text.Trim(), port);

                NetworkStream stream = tcpClient.GetStream();
                reader = new StreamReader(stream, new UTF8Encoding(false));
                writer = new StreamWriter(stream, new UTF8Encoding(false));
                writer.AutoFlush = true;
                isConnected = true;

                receiveThread = new Thread(ReceiveLoop);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                Send("LOGIN\t" + txtName.Text.Trim());
                lblTrangThai.Text = "Đang đăng nhập...";
            }
            catch (Exception ex)
            {
                CloseConnection();
                MessageBox.Show("Không kết nối được tới server: " + ex.Message);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Send("QUIT");
            CloseConnection();
            SetConnectedState(false);
            lblTrangThai.Text = "Đã ngắt kết nối.";
        }

        private void Bai5Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConnected) Send("QUIT");
            CloseConnection();
        }

        private void CloseConnection()
        {
            isConnected = false;
            try { if (writer != null) writer.Close(); } catch { }
            try { if (reader != null) reader.Close(); } catch { }
            try { if (tcpClient != null) tcpClient.Close(); } catch { }
            writer = null;
            reader = null;
            tcpClient = null;
        }

        private void SetConnectedState(bool connected)
        {
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            txtIp.Enabled = txtPort.Enabled = txtName.Enabled = !connected;
            txtMonAn.Enabled = btnThem.Enabled = btnXoa.Enabled = connected;
            btnNgauNhien.Enabled = rbCuaToi.Enabled = rbCongDong.Enabled = connected;
            if (!connected)
            {
                lvMonAn.Items.Clear();
                lblKetQua.Text = "";
            }
        }

        private void Send(string message)
        {
            try
            {
                if (writer != null)
                {
                    lock (writer) { writer.WriteLine(message); }
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
        }

        private string CurrentScope()
        {
            return rbCuaToi.Checked ? "ME" : "ALL";
        }

        private void RequestList()
        {
            Send("LIST\t" + CurrentScope());
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            string tenMon = txtMonAn.Text.Trim();
            if (tenMon.Length == 0)
            {
                MessageBox.Show("Hãy nhập tên món ăn.");
                return;
            }
            Send("ADD\t" + tenMon);
            txtMonAn.Clear();
            txtMonAn.Focus();
        }

        private void TxtMonAn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnThem_Click(sender, EventArgs.Empty);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (lvMonAn.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn món cần xóa.");
                return;
            }
            Send("DELETE\t" + lvMonAn.SelectedItems[0].Tag);
        }

        private void BtnNgauNhien_Click(object sender, EventArgs e)
        {
            Send("RANDOM\t" + CurrentScope());
        }

        private void Scope_CheckedChanged(object sender, EventArgs e)
        {
           
            if (isConnected && ((RadioButton)sender).Checked)
                RequestList();
        }


        private void ReceiveLoop()
        {
            try
            {
                string line;
                while (isConnected && (line = reader.ReadLine()) != null)
                {
                    string received = line;
                    RunOnUi(() => HandleReply(received));
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
            catch (NullReferenceException) { }

            if (isConnected)
            {
                RunOnUi(() =>
                {
                    CloseConnection();
                    SetConnectedState(false);
                    lblTrangThai.Text = "Mất kết nối tới server.";
                });
            }
        }

  
        private void RunOnUi(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            if (InvokeRequired)
            {
                try { Invoke(action); }
                catch (ObjectDisposedException) { }
            }
            else
            {
                action();
            }
        }

        private void HandleReply(string line)
        {
            string[] parts = line.Split('\t');

            switch (parts[0])
            {
                case "OK":
                    lblTrangThai.Text = parts[2];
                    if (parts[1] == "LOGIN") SetConnectedState(true);
                    RequestList();
                    break;

                case "ERR":
                    lblTrangThai.Text = parts[1];
                    if (!btnDisconnect.Enabled) 
                    {
                        CloseConnection();
                        SetConnectedState(false);
                    }
                    MessageBox.Show(parts[1], "Thông báo");
                    break;

                case "LIST":
                    if (parts[1] == CurrentScope()) FillList(parts);
                    break;

                case "RANDOM":
                    lblKetQua.Text = parts[2] + "  (do " + parts[3] + " thêm)";
                    break;

                case "EMPTY":
                    lblKetQua.Text = "Chưa có món nào để chọn, hãy thêm món trước!";
                    break;
            }
        }

        private void FillList(string[] parts)
        {
            lvMonAn.BeginUpdate();
            lvMonAn.Items.Clear();
            int stt = 1;
            for (int i = 2; i + 2 < parts.Length; i += 3)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(parts[i + 1]);
                item.SubItems.Add(parts[i + 2]);
                item.Tag = parts[i]; 
                lvMonAn.Items.Add(item);
                stt++;
            }
            lvMonAn.EndUpdate();
        }
    }
}
