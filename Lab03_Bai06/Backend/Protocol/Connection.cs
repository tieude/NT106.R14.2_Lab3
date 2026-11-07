using Lab03_Bai06;
using System.IO;
using System.Net.Sockets;
using System.Text;
namespace Lab03_Bai06
{
    public class Connection
    {
        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;
        private readonly BinaryReader reader;
        private readonly object sendLock = new object();
        public string UserName { get; set; }
        public Connection(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            stream = tcpClient.GetStream();
            reader = new BinaryReader(stream, Encoding.UTF8);
        }
        public Packet Receive()
        {
            return Packet.ReadFrom(reader);
        }
        public void Send(Packet packet)
        {
            byte[] bytes = packet.ToBytes();
            lock (sendLock)
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        /// <summary>Gửi nhưng không ném lỗi, trả về false nếu thất bại.</summary>
        public bool TrySend(Packet packet)
        {
            try { Send(packet); return true; }
            catch { return false; }
        }
        public void Close()
        {
            try { stream.Close(); } catch { }
            try { tcpClient.Close(); } catch { }
        }
    }
}