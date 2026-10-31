using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class PhimDto
    {
        [JsonPropertyName("MaPhim")]
        public int MaPhim { get; set; }

        [JsonPropertyName("TenPhim")]
        public string TenPhim { get; set; }

        [JsonPropertyName("GiaGoc")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double GiaGoc { get; set; }

        public override string ToString() => TenPhim;
    }

    public class PhongDto
    {
        [JsonPropertyName("MaPhong")]
        public int MaPhong { get; set; }

        [JsonPropertyName("TenPhong")]
        public string TenPhong { get; set; }

        [JsonPropertyName("MaPhim")]
        public int MaPhim { get; set; }

        public override string ToString() => TenPhong;
    }

    public class GheDto
    {
        [JsonPropertyName("MaGhe")]
        public int MaGhe { get; set; }

        [JsonPropertyName("TenGhe")]
        public string TenGhe { get; set; }

        [JsonPropertyName("LoaiGhe")]
        public int LoaiGhe { get; set; }

        [JsonPropertyName("TTGhe")]
        public int TTGhe { get; set; }

        [JsonPropertyName("GiaGoc")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double GiaGoc { get; set; }

        public override string ToString() => TenGhe;
    }

    public class BookRequestDto
    {
        [JsonPropertyName("MaGheList")]
        public List<int> MaGheList { get; set; }
    }

    public class BookResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("TongTien")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal TongTien { get; set; }
    }
}