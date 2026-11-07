using Lab03_Bai06;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab03_Bai06
{
    public class ChatServer
    {
        private TcpListener listener;
        private volatile bool isRunning;

        // "Sổ địa chỉ": tên người dùng -> kết nối
        private readonly Dictionary<string, Connection> clients = new Dictionary<string, Connection>();
        private readonly object clientsLock = new object();

        /// <summary>Có dòng log mới (được gọi từ luồng phụ).</summary>
        public event Action<string> LogReceived;

        /// <summary>Danh sách người online thay đổi (được gọi từ luồng phụ).</summary>
        public event Action<string[]> OnlineListChanged;

        public bool IsRunning => isRunning;

        // ===================== BẬT / TẮT SERVER =====================

        /// <summary>Bắt đầu lắng nghe. Ném SocketException nếu không mở được cổng.</summary>
        public void Start(IPAddress ip, int port)
        {
            if (isRunning) return;

            listener = new TcpListener(ip, port);
            listener.Start();

            isRunning = true;
            new Thread(AcceptLoop) { IsBackground = true }.Start();
            Log($"Server đang lắng nghe tại {ip}:{port}");
        }

        public void Stop()
        {
            if (!isRunning) return;
            isRunning = false;
            listener.Stop(); // Làm AcceptTcpClient() ném lỗi -> AcceptLoop kết thúc

            List<Connection> all;
            lock (clientsLock)
            {
                all = clients.Values.ToList();
                clients.Clear();
            }
            foreach (Connection c in all)
            {
                c.TrySend(Notice("Server đã dừng."));
                c.Close();
            }

            Log("Server đã dừng.");
            RaiseOnlineListChanged();
        }

        private void AcceptLoop()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient tcp = listener.AcceptTcpClient(); // Chờ client mới
                    new Thread(() => HandleClient(tcp)) { IsBackground = true }.Start();
                }
                catch (SocketException) { break; }
                catch (ObjectDisposedException) { break; }
            }
        }

        private void HandleClient(TcpClient tcp)
        {
            var conn = new Connection(tcp);
            string userName = null;

            try
            {
                Packet join = conn.Receive();
                string requested = (join.Sender ?? "").Trim();

                if (join.Type != PacketType.Join || requested == "" || requested.Contains("|"))
                {
                    conn.TrySend(new Packet { Type = PacketType.JoinFail, Text = "Tên không hợp lệ." });
                    return;
                }
                if (!TryAddClient(requested, conn))
                {
                    conn.TrySend(new Packet { Type = PacketType.JoinFail, Text = $"Tên \"{requested}\" đã có người dùng." });
                    return;
                }

                userName = requested;
                conn.Send(new Packet { Type = PacketType.JoinOk });
                Log($"{userName} đã kết nối ({tcp.Client.RemoteEndPoint}).");
                Broadcast(Notice($"{userName} đã tham gia phòng chat."), userName);
                BroadcastUserList();
                RaiseOnlineListChanged();

                while (isRunning)
                {
                    Packet packet = conn.Receive();
                    packet.Sender = userName;

                    if (packet.Type == PacketType.Leave) break;

                    if (packet.Type == PacketType.Text ||
                        packet.Type == PacketType.Image ||
                        packet.Type == PacketType.File)
                    {
                        Route(packet, conn);
                    }
                }
            }
            catch (IOException) { }                
            catch (ObjectDisposedException) { }
            catch (InvalidDataException) { }       
            finally
            {
                if (userName != null && RemoveClient(userName))
                {
                    Log($"{userName} đã ngắt kết nối.");
                    Broadcast(Notice($"{userName} đã rời phòng chat."), null);
                    BroadcastUserList();
                    RaiseOnlineListChanged();
                }
                conn.Close();
            }
        }

        private void Route(Packet packet, Connection from)
        {
            string content = packet.Type == PacketType.Text
                ? $"\"{packet.Text}\""
                : $"{packet.Type} '{packet.FileName}' ({packet.Data.Length / 1024} KB)";

            if (string.IsNullOrEmpty(packet.Receiver))          
            {
                Broadcast(packet, from.UserName);
                Log($"{from.UserName} → Mọi người: {content}");
            }
            else if (SendTo(packet.Receiver, packet))           
            {
                Log($"{from.UserName} → {packet.Receiver} (riêng): {content}");
            }
            else
            {
                from.TrySend(Notice($"Không gửi được: {packet.Receiver} không còn online."));
            }
        }
        private void Broadcast(Packet packet, string excludeUser)
        {
            List<Connection> targets;
            lock (clientsLock)
            {
                targets = clients.Values.Where(c => c.UserName != excludeUser).ToList();
            }
            foreach (Connection c in targets)
                c.TrySend(packet);
        }

        private bool SendTo(string userName, Packet packet)
        {
            Connection target;
            lock (clientsLock)
            {
                clients.TryGetValue(userName, out target);
            }
            return target != null && target.TrySend(packet);
        }

        private void BroadcastUserList()
        {
            string list;
            lock (clientsLock)
            {
                list = string.Join("|", clients.Keys);
            }
            Broadcast(new Packet { Type = PacketType.UserList, Text = list }, null);
        }
        private bool TryAddClient(string userName, Connection conn)
        {
            lock (clientsLock)
            {
                if (clients.ContainsKey(userName)) return false;
                conn.UserName = userName;
                clients.Add(userName, conn);
                return true;
            }
        }

        private bool RemoveClient(string userName)
        {
            lock (clientsLock)
            {
                return clients.Remove(userName);
            }
        }

        private static Packet Notice(string text)
        {
            return new Packet { Type = PacketType.Notice, Text = text };
        }

        private void Log(string message)
        {
            LogReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        private void RaiseOnlineListChanged()
        {
            string[] names;
            lock (clientsLock)
            {
                names = clients.Keys.ToArray();
            }
            OnlineListChanged?.Invoke(names);
        }
    }
}
