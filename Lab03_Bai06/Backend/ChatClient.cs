using Lab03_Bai06;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab03_Bai06
{
    public class ChatClient
    {
        private Connection connection;

        public string UserName { get; private set; }
        public bool IsConnected => connection != null;

        public event Action<Packet> PacketReceived;

        public event Action Disconnected;

        public bool Connect(IPAddress ip, int port, string userName, out string errorMessage)
        {
            errorMessage = null;
            Connection conn = null;

            try
            {
                var tcp = new TcpClient();
                tcp.Connect(ip, port);
                conn = new Connection(tcp);

                conn.Send(new Packet { Type = PacketType.Join, Sender = userName });
                Packet reply = conn.Receive();

                if (reply.Type != PacketType.JoinOk)
                {
                    errorMessage = string.IsNullOrEmpty(reply.Text)
                        ? "Server từ chối kết nối."
                        : reply.Text;
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                errorMessage = "Không kết nối được tới server: " + ex.Message;
                conn?.Close();
                return false;
            }

            connection = conn;
            UserName = userName;

            new Thread(() => ReceiveLoop(conn)) { IsBackground = true }.Start();
            return true;
        }

        public void Disconnect()
        {
            Connection conn = connection;
            if (conn == null) return;

            conn.TrySend(new Packet { Type = PacketType.Leave });
            conn.Close(); 
        }

        private void ReceiveLoop(Connection conn)
        {
            try
            {
                while (true)
                {
                    Packet packet = conn.Receive();
                    PacketReceived?.Invoke(packet);
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
            catch (InvalidDataException) { }
            finally
            {
                conn.Close();
                if (connection == conn)
                {
                    connection = null;
                    Disconnected?.Invoke();
                }
            }
        }

        public bool SendText(string receiver, string text, out string errorMessage)
        {
            return SendPacket(new Packet
            {
                Type = PacketType.Text,
                Sender = UserName,
                Receiver = receiver ?? "",
                Text = text
            }, out errorMessage);
        }

        public bool SendFileData(PacketType type, string receiver, string fileName, byte[] data, out string errorMessage)
        {
            return SendPacket(new Packet
            {
                Type = type,
                Sender = UserName,
                Receiver = receiver ?? "",
                FileName = fileName,
                Data = data
            }, out errorMessage);
        }

        private bool SendPacket(Packet packet, out string errorMessage)
        {
            errorMessage = null;
            Connection conn = connection;
            if (conn == null)
            {
                errorMessage = "Chưa kết nối tới server.";
                return false;
            }

            try
            {
                conn.Send(packet);
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is ObjectDisposedException)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
