using Newtonsoft.Json;

namespace StarScope
{
    internal class Service
    {
        public static readonly HttpClient client = new HttpClient();
        private const string API_URL = "https://v2.xxapi.cn/api/horoscope";

        public async Task<string> GetHoroscope(string type, string time)
        {
            var response = await client.GetStringAsync($"{API_URL}?consName={type}&type={time}");
        }
    }
}
