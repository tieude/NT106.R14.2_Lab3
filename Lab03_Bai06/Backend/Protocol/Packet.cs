using System.IO;
using System.Text;
namespace Lab03_Bai06
{
    /// <summary>Các loại gói tin trong giao thức chat.</summary>
    public enum PacketType : byte
    {
        Join = 1,  
        JoinOk,    
        JoinFail,  
        UserList,  
        Text,      
        Image,      
        File,       
        Notice,     
        Leave      
    }
    public class Packet
    {
        public const int MaxDataSize = 10 * 1024 * 1024; // Giới hạn 10 MB
        public PacketType Type { get; set; }
        public string Sender { get; set; } = "";
        public string Receiver { get; set; } = "";
        public string Text { get; set; } = "";
        public string FileName { get; set; } = "";
        public byte[] Data { get; set; } = new byte[0];
        /// <summary>Chuyển Packet thành mảng byte để gửi đi trong một lần ghi.</summary>
        public byte[] ToBytes()
        {
            using (var memory = new MemoryStream())
            using (var writer = new BinaryWriter(memory, Encoding.UTF8))
            {
                byte[] data = Data ?? new byte[0];
                writer.Write((byte)Type);
                writer.Write(Sender ?? "");      // BinaryWriter tự ghi [độ dài][UTF-8]
                writer.Write(Receiver ?? "");
                writer.Write(Text ?? "");
                writer.Write(FileName ?? "");
                writer.Write(data.Length);       // Ghi độ dài trước...
                writer.Write(data);              // ...rồi mới ghi dữ liệu
                writer.Flush();
                return memory.ToArray();
            }
        }
        /// <summary>Đọc đúng MỘT Packet từ stream (chặn cho tới khi đủ byte).</summary>
        public static Packet ReadFrom(BinaryReader reader)
        {
            var packet = new Packet
            {
                Type = (PacketType)reader.ReadByte(),
                Sender = reader.ReadString(),
                Receiver = reader.ReadString(),
                Text = reader.ReadString(),
                FileName = reader.ReadString()
            };
            int length = reader.ReadInt32();
            if (length < 0 || length > MaxDataSize)
                throw new InvalidDataException("Kích thước dữ liệu không hợp lệ: " + length);
            packet.Data = reader.ReadBytes(length);
            if (packet.Data.Length != length)
                throw new EndOfStreamException("Kết nối bị đóng giữa chừng.");
            return packet;
        }
    }
}