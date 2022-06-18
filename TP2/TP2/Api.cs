using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP2 {
    public class Api {
        public static async Task<CurrentWeather> GetDataFromApi(string location) {
            var client = new HttpClient();
            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=43db2aeef2a8280cb1c107389901a6b2")
            };
            using (var response = await client.SendAsync(request)) {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                CurrentWeather CurrentWeather = JsonSerializer.Deserialize<CurrentWeather>(body);
                return CurrentWeather;
            }
        }
    }
}
