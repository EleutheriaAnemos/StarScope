using Newtonsoft.Json.Linq;

namespace StarScope
{
    internal class Service
    {
        public static readonly HttpClient client = new HttpClient();
        private const string API_URL = "https://v2.xxapi.cn/api/horoscope";

        private static readonly Dictionary<string, string> ConstellationMap = new Dictionary<string, string>
        {
            {"白羊座", "aries"},
            {"金牛座", "taurus"},
            {"双子座", "gemini"},
            {"巨蟹座", "cancer"},
            {"狮子座", "leo"},
            {"处女座", "virgo"},
            {"天秤座", "libra"},
            {"天蝎座", "scorpio"},
            {"射手座", "sagittarius"},
            {"摩羯座", "capricorn"},
            {"水瓶座", "aquarius"},
            {"双鱼座", "pisces"}
        };
        private static readonly Dictionary<string, string> TimeMap = new Dictionary<string, string>
        {
            {"今日", "today"},
            {"明日", "nextday"},
            {"一周", "week"},
            {"一月", "month"}
        };
        public async Task<string> GetHoroscopeAsync(string chineseConstellation, string chineseTime)
        {
            if (!ConstellationMap.TryGetValue(chineseConstellation, out string englishConstellation))
                throw new Exception("无法识别的星座");
            if (!TimeMap.TryGetValue(chineseTime, out string englishTime))
                throw new Exception("无法识别的时间范围");

            var url = $"{API_URL}?type={englishConstellation}&time={englishTime}";
            var responseString = await client.GetStringAsync(url);

            // 解析 JSON
            var jObject = JObject.Parse(responseString);
            if (jObject["code"]?.Value<int>() != 200)
            {
                throw new Exception("API返回错误: " + jObject["msg"]?.ToString());
            }

            var fortuneText = jObject["data"]?["fortunetext"];
            var todo = jObject["data"]?["todo"];

            if (fortuneText == null || todo == null)
            {
                throw new Exception("返回数据格式异常");
            }

            // 拼接展示内容
            string result = "";

            result += "【综合运势】\n" + fortuneText["all"]?.ToString() + "\n";
            result += "【健康运势】\n" + fortuneText["health"]?.ToString() + "\n";
            result += "【爱情运势】\n" + fortuneText["love"]?.ToString() + "\n";
            result += "【财富运势】\n" + fortuneText["money"]?.ToString() + "\n";
            result += "【事业学业】\n" + fortuneText["work"]?.ToString() + "\n";
            result += "【宜】\n" + todo["yi"]?.ToString() + "\n";
            result += "【忌】\n" + todo["ji"]?.ToString() + "\n";

            return result;
        }
    }
}
