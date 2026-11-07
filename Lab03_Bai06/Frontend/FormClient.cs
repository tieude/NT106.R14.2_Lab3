using Lab03_Bai06;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Lab03_Bai06
{
    public partial class FormClient : Form
    {
        private const string EveryoneItem = "[ Mọi người ]";

        private readonly ChatClient client = new ChatClient();

        private string CurrentReceiver =>
            lstUsers.SelectedIndex > 0 ? lstUsers.SelectedItem.ToString() : "";

        public FormClient()
        {
            InitializeComponent();

            btnConnect.Click += btnConnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;
            btnSend.Click += btnSend_Click;
            btnSendImage.Click += btnSendImage_Click;
            btnSendFile.Click += btnSendFile_Click;
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            FormClosing += (s, e) => client.Disconnect();

            client.PacketReceived += OnPacketReceived;
            client.Disconnected += OnClientDisconnected;

            AcceptButton = btnSend; // Nhấn Enter = bấm Gửi
            SetConnectedState(false);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name == "" || name.Contains("|"))
            {
                MessageBox.Show("Tên không được để trống và không chứa ký tự '|'.");
                return;
            }
            if (!IPAddress.TryParse(txtServerIP.Text.Trim(), out IPAddress ip))
            {
                MessageBox.Show("Địa chỉ IP không hợp lệ!");
                return;
            }

            if (!client.Connect(ip, (int)numPort.Value, name, out string error))
            {
                MessageBox.Show(error, "Không thể vào phòng");
                return;
            }

            SetConnectedState(true);
            AddNotice($"Đã vào phòng chat với tên \"{name}\".");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
        }
        private void OnPacketReceived(Packet p)
        {
            RunOnUI(() =>
            {
                switch (p.Type)
                {
                    case PacketType.UserList: UpdateUserList(p.Text); break;
                    case PacketType.Notice: AddNotice(p.Text); break;
                    case PacketType.Text: AddTextMessage(p.Sender, p.Receiver, p.Text); break;
                    case PacketType.Image: AddImageMessage(p.Sender, p.Receiver, p.FileName, p.Data); break;
                    case PacketType.File: AddFileMessage(p.Sender, p.Receiver, p.FileName, p.Data); break;
                }
            });
        }

        private void OnClientDisconnected()
        {
            RunOnUI(() =>
            {
                lstUsers.Items.Clear();
                SetConnectedState(false);
                AddNotice("Đã ngắt kết nối khỏi server.");
            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = txtMessage.Text.Trim();
            if (text == "" || !client.IsConnected) return;

            string receiver = CurrentReceiver;
            if (!client.SendText(receiver, text, out string error))
            {
                AddNotice("Gửi thất bại: " + error);
                return;
            }

            AddTextMessage(client.UserName, receiver, text); 
            txtMessage.Clear();
            txtMessage.Focus();
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            SelectAndSendFile(PacketType.Image, "Ảnh (*.jpg;*.png)|*.jpg;*.jpeg;*.png");
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            SelectAndSendFile(PacketType.File, "Text file (*.txt)|*.txt");
        }

        private void SelectAndSendFile(PacketType type, string filter)
        {
            if (!client.IsConnected) return;

            using (var dialog = new OpenFileDialog { Filter = filter })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var info = new FileInfo(dialog.FileName);
                if (info.Length > Packet.MaxDataSize)
                {
                    MessageBox.Show("File quá lớn (tối đa 10 MB).");
                    return;
                }

                byte[] data = File.ReadAllBytes(info.FullName);
                string receiver = CurrentReceiver;

                if (!client.SendFileData(type, receiver, info.Name, data, out string error))
                {
                    AddNotice("Gửi thất bại: " + error);
                    return;
                }

                if (type == PacketType.Image)
                    AddImageMessage(client.UserName, receiver, info.Name, data);
                else
                    AddFileMessage(client.UserName, receiver, info.Name, data);
            }
        }

        private void UpdateUserList(string list)
        {
            string previous = CurrentReceiver;
            string[] users = list.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            lstUsers.BeginUpdate();
            lstUsers.Items.Clear();
            lstUsers.Items.Add(EveryoneItem);
            foreach (string user in users)
            {
                if (user != client.UserName) lstUsers.Items.Add(user); 
            }
            int index = lstUsers.Items.IndexOf(previous);
            lstUsers.SelectedIndex = index > 0 ? index : 0; 
            lstUsers.EndUpdate();
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTarget.Text = CurrentReceiver == ""
                ? "Đang gửi tới: Mọi người"
                : $"Đang nhắn riêng với: {CurrentReceiver}";
        }

        private void AddChatControl(Control control)
        {
            flpChat.Controls.Add(control);
            flpChat.ScrollControlIntoView(control);
        }

        private Label CreateLabel(string text, Color color, FontStyle style = FontStyle.Regular)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = color,
                Font = new Font("Segoe UI", 10f, style),
                MaximumSize = new Size(flpChat.ClientSize.Width - 40, 0), 
                Margin = new Padding(6, 4, 6, 0)
            };
        }

        private string BuildHeader(string sender, string receiver)
        {
            string time = DateTime.Now.ToString("HH:mm");
            bool isMine = sender == client.UserName;
            string from = isMine ? "Bạn" : sender;

            if (string.IsNullOrEmpty(receiver)) return $"[{time}] {from}:";
            string to = isMine ? receiver : "Bạn";
            return $"[{time}] (Riêng) {from} → {to}:";
        }

        private Color GetColor(string sender, string receiver)
        {
            if (!string.IsNullOrEmpty(receiver)) return Color.DarkMagenta; // Tin riêng
            return sender == client.UserName ? Color.RoyalBlue : Color.Black;
        }

        private void AddNotice(string text)
        {
            AddChatControl(CreateLabel("— " + text + " —", Color.Gray, FontStyle.Italic));
        }

        private void AddTextMessage(string sender, string receiver, string text)
        {
            AddChatControl(CreateLabel(BuildHeader(sender, receiver) + " " + text, GetColor(sender, receiver)));
        }

        private void AddImageMessage(string sender, string receiver, string fileName, byte[] data)
        {
            Image image;
            try
            {
                using (var memory = new MemoryStream(data))
                using (var temp = Image.FromStream(memory))
                {
                    image = new Bitmap(temp);
                }
            }
            catch (ArgumentException)
            {
                AddNotice($"Không đọc được ảnh {fileName}.");
                return;
            }

            AddChatControl(CreateLabel(BuildHeader(sender, receiver) + " [Ảnh] " + fileName, GetColor(sender, receiver)));

            var pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(220, 160),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(20, 2, 6, 6)
            };
            pictureBox.Click += (s, e) => SaveReceivedData(fileName, data, "Ảnh|*.jpg;*.jpeg;*.png");
            AddChatControl(pictureBox);
        }

        private void AddFileMessage(string sender, string receiver, string fileName, byte[] data)
        {
            AddChatControl(CreateLabel(BuildHeader(sender, receiver), GetColor(sender, receiver)));

            var link = new LinkLabel
            {
                Text = $"[File] {fileName} ({data.Length / 1024.0:0.0} KB) - nhấn để lưu",
                AutoSize = true,
                Font = new Font("Segoe UI", 10f),
                Margin = new Padding(20, 2, 6, 6)
            };
            link.LinkClicked += (s, e) => SaveReceivedData(fileName, data, "Text file|*.txt");
            AddChatControl(link);
        }

        private void SaveReceivedData(string fileName, byte[] data, string filter)
        {
            using (var dialog = new SaveFileDialog { FileName = fileName, Filter = filter + "|Tất cả|*.*" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                File.WriteAllBytes(dialog.FileName, data);
                AddNotice("Đã lưu: " + dialog.FileName);
            }
        }

        private void SetConnectedState(bool connected)
        {
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            txtServerIP.Enabled = numPort.Enabled = txtName.Enabled = !connected;
            txtMessage.Enabled = btnSend.Enabled = btnSendImage.Enabled =
                btnSendFile.Enabled = lstUsers.Enabled = connected;
            Text = connected ? $"Chat Client - {client.UserName}" : "Chat Client";
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
