using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
   
    public class Bai5Server : Form
    {
        private class ClientSession
        {
            public int UserId = -1;
            public string UserName;
        }

        private TextBox txtPort;
        private Button btnListen;
        private Button btnStop;
        private ListView lvLog;

        private TcpListener listener;
        private Thread acceptThread;
        private volatile bool isRunning;
        private Bai5Database database;

        public Bai5Server()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            Text = "Bai5 - Server Hôm nay ăn gì?";
            ClientSize = new Size(560, 420);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblPort = new Label();
            lblPort.Text = "Port:";
            lblPort.Location = new Point(15, 20);
            lblPort.AutoSize = true;

            txtPort = new TextBox();
            txtPort.Text = "8080";
            txtPort.Location = new Point(60, 16);
            txtPort.Size = new Size(70, 23);

            btnListen = new Button();
            btnListen.Text = "Listen";
            btnListen.Location = new Point(360, 14);
            btnListen.Size = new Size(90, 27);
            btnListen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnListen.Click += BtnListen_Click;

            btnStop = new Button();
            btnStop.Text = "Stop";
            btnStop.Location = new Point(455, 14);
            btnStop.Size = new Size(90, 27);
            btnStop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStop.Enabled = false;
            btnStop.Click += BtnStop_Click;

            lvLog = new ListView();
            lvLog.View = View.Details;
            lvLog.FullRowSelect = true;
            lvLog.Columns.Add("Thời gian", 90);
            lvLog.Columns.Add("Nhật ký server", 440);
            lvLog.Location = new Point(15, 55);
            lvLog.Size = new Size(530, 350);
            lvLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Controls.Add(lblPort);
            Controls.Add(txtPort);
            Controls.Add(btnListen);
            Controls.Add(btnStop);
            Controls.Add(lvLog);

            FormClosing += Bai5Server_FormClosing;
        }


        private void BtnListen_Click(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Port không hợp lệ (1 - 65535).");
                return;
            }

            try
            {
                database = new Bai5Database(Path.Combine(Application.StartupPath, "monan_server.db"));
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khởi động server: " + ex.Message);
                return;
            }

            isRunning = true;
            acceptThread = new Thread(AcceptLoop);
            acceptThread.IsBackground = true;
            acceptThread.Start();

            btnListen.Enabled = false;
            btnStop.Enabled = true;
            txtPort.Enabled = false;
            Log("Server started on port " + port + "!");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopServer();
            btnListen.Enabled = true;
            btnStop.Enabled = false;
            txtPort.Enabled = true;
            Log("Server stopped.");
        }

        private void StopServer()
        {
            isRunning = false;
            if (listener != null)
            {
                listener.Stop(); 
                listener = null;
            }
        }

        private void Bai5Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }


        private void AcceptLoop()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (SocketException) { break; }
                catch (InvalidOperationException) { break; }
                catch (NullReferenceException) { break; }
            }
        }

        private void HandleClient(TcpClient client)
        {
            string endPoint = client.Client.RemoteEndPoint.ToString();
            Log("New client connected from " + endPoint);
            ClientSession session = new ClientSession();

            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream, new UTF8Encoding(false)))
                using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)))
                {
                    writer.AutoFlush = true;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string reply = ProcessCommand(line, session);
                        if (reply == null) break; 
                        writer.WriteLine(reply);
                    }
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }

            Log((session.UserName ?? endPoint) + " disconnected.");
        }

     
        private string ProcessCommand(string line, ClientSession session)
        {
            string[] parts = line.Split('\t');
            string command = parts[0].ToUpper();

            try
            {
                if (command == "QUIT")
                    return null;

                if (command == "LOGIN")
                {
                    string name = parts.Length > 1 ? Clean(parts[1]) : "";
                    if (name.Length == 0) return "ERR\tTên người dùng không được để trống.";
                    session.UserId = database.GetOrCreateUser(name);
                    session.UserName = name;
                    Log(name + " logged in.");
                    return "OK\tLOGIN\tXin chào " + name + "!";
                }

                if (session.UserId < 0)
                    return "ERR\tBạn chưa đăng nhập.";

                switch (command)
                {
                    case "ADD":
                        return HandleAdd(parts, session);
                    case "DELETE":
                        return HandleDelete(parts, session);
                    case "LIST":
                        return HandleList(parts, session);
                    case "RANDOM":
                        return HandleRandom(parts, session);
                    default:
                        return "ERR\tLệnh không hợp lệ.";
                }
            }
            catch (Exception ex)
            {
                return "ERR\tLỗi server: " + Clean(ex.Message);
            }
        }

        private string HandleAdd(string[] parts, ClientSession session)
        {
            string tenMon = parts.Length > 1 ? Clean(parts[1]) : "";
            if (tenMon.Length == 0) return "ERR\tTên món ăn không được để trống.";

            if (!database.AddMonAn(tenMon, session.UserId))
                return "ERR\tBạn đã thêm món \"" + tenMon + "\" rồi.";

            Log(session.UserName + " added: " + tenMon);
            return "OK\tADD\tĐã thêm món \"" + tenMon + "\".";
        }

        private string HandleDelete(string[] parts, ClientSession session)
        {
            int id;
            if (parts.Length < 2 || !int.TryParse(parts[1], out id))
                return "ERR\tId món ăn không hợp lệ.";

            if (!database.DeleteMonAn(id, session.UserId))
                return "ERR\tKhông xóa được (món không tồn tại hoặc không phải của bạn).";

            Log(session.UserName + " deleted dish #" + id);
            return "OK\tDELETE\tĐã xóa món ăn.";
        }

        private string HandleList(string[] parts, ClientSession session)
        {
            string scope = GetScope(parts);
            int? userId = scope == "ME" ? (int?)session.UserId : null;

            StringBuilder reply = new StringBuilder("LIST\t" + scope);
            foreach (MonAnInfo mon in database.GetMonAn(userId))
                reply.Append("\t" + mon.Id + "\t" + mon.TenMonAn + "\t" + mon.NguoiThem);
            return reply.ToString();
        }

        private string HandleRandom(string[] parts, ClientSession session)
        {
            string scope = GetScope(parts);
            int? userId = scope == "ME" ? (int?)session.UserId : null;

            MonAnInfo mon = database.GetRandomMonAn(userId);
            if (mon == null) return "EMPTY\t" + scope;

            Log(session.UserName + " random (" + scope + "): " + mon.TenMonAn);
            return "RANDOM\t" + scope + "\t" + mon.TenMonAn + "\t" + mon.NguoiThem;
        }

        private static string GetScope(string[] parts)
        {
            return parts.Length > 1 && parts[1].ToUpper() == "ME" ? "ME" : "ALL";
        }

       
        private static string Clean(string text)
        {
            return text.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ').Trim();
        }


        private void Log(string message)
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (InvokeRequired)
            {
                try { Invoke(new Action<string>(Log), message); }
                catch (ObjectDisposedException) { }
                return;
            }

            ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
            item.SubItems.Add(message);
            lvLog.Items.Add(item);
            item.EnsureVisible();
        }
    }
}
