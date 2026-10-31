using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DTO;

namespace api1
{
    public class api2
    {
        private static readonly String serverIp = "127.0.0.1";
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri($"http://{serverIp}:5000/") };
        public static async Task<List<PhimDto>> GetMoviesAsync()
        {
            var response = await client.GetAsync("api/movies");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PhimDto>>(json);
        }
        public static async Task<List<PhongDto>> GetRoomAsync(int maPhim)
        {
            var response = await client.GetAsync($"api/room?MaPhim={maPhim}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PhongDto>>(json);
        }
        public static async Task<List<GheDto>> GetSeatAsync(int maPhong)
        {
            var response = await client.GetAsync($"api/Ghe?MaPhong={maPhong}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GheDto>>(json);
        }
        public static async Task<BookResponseDto> BookSeatsAsync(List<int> maGheList)
        {
            var requestBody = new BookRequestDto { MaGheList = maGheList };
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/book", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookResponseDto>(jsonResponse);
        }

    }
}
