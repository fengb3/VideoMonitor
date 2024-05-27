using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using Model.RunTimeVar;
using MyLib.API;
using MyLib.DataManagement;
using MyLib.Helper;
using MyLib.Tool;

namespace MyBiliBiliMonitor_2;

public class APIHelper
{
    #region Client

    private static HttpClientHandler _httpClientHandler = null!;
    private static HttpClient        _httpClient        = null!;

    private static HttpClient Client
    {
        get
        {
            if (_httpClient == null!)
            {
                _httpClientHandler                 = new HttpClientHandler();
                _httpClientHandler.CookieContainer = new CookieContainer();
                // 配置 HttpClientHandler
                // TODO: 从配置文件读取
                _httpClientHandler.UseCookies = true;
                // _httpClientHandler.CookieContainer.Add(new Uri("https://api.bilibili.com"),
                //                                        new Cookie("SESSDATA",
                //                                                   "d569d525%2C1715446637%2C874c4%2Ab1CjC88ULRe5CC7ZozcKncPLkgk6k6uIPdIcmis5NOlGYEkMMFg-GO4AjMmgQOuK25Wj0SVjU3NDVsak5BZmhITFNlV2dNYlNmZzRyRnNhT0x2Tk9NS2syS284Q3lOS3FRdGN6bFdTcS1wX1prM3d1M3YwT0c0cjIyWnVUSkVVNXZVTmhnSWVUMm9nIIEC"));
                _httpClientHandler.CookieContainer.Add(new Uri("https://api.bilibili.com"),
                                                       new Cookie("SESSDATA",
                                                                  "04f1865e%2C1715496409%2Cf05aa%2Ab2CjDgb0p_Ice-UKQLjrdSWOcviZ9yIvTK0pFNx85Y39AVyD7Gtq67y6xnxb1jlXYkOjoSVnpocWx1UGs4RzEtdHNDVUM5elZOY3hmOHlObmx0RzYxZ2tUWjFJdjBpeUpiQUMzMXkwWUE3enNGVjdHRU1BRkNudUFlazVZcTNhTTZFcmRnRENJWXJRIIEC"));
                _httpClientHandler.CookieContainer.Add(new Uri("https://api.bilibili.com"),
                                                       new Cookie(
                                                           "buvid3",
                                                           "1A55B5FE-C4AF-28C0-8C4E-B7423B8D650B95694infoc"));
                _httpClientHandler.CookieContainer.Add(new Uri("https://api.bilibili.com"),
                                                       new Cookie(
                                                           "buvid4",
                                                           "125953A5-B66A-FB59-8F31-07DCBECDC61897239-023111400-g%2FJDFC92Uz%2FjiQi2iakt2g%3D%3D"));

                _httpClient = new HttpClient(_httpClientHandler);
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"); // 浏览器的User-Agent
            }

            return _httpClient;
        }
    }

    #endregion
    
    #region WBI Signatures

    private static readonly int[] MixinKeyEncTab =
    {
        46, 47, 18, 2, 53, 8, 23, 32, 15, 50, 10, 31, 58, 3, 45, 35, 27, 43, 5, 49, 33, 9, 42, 19, 29, 28, 14, 39,
        12, 38, 41, 13, 37, 48, 7, 16, 24, 55, 40, 61, 26, 17, 0, 1, 60, 51, 30, 4, 22, 25, 54, 21, 56, 59, 6, 63,
        57, 62, 11, 36, 20, 34, 44, 52
    };

    //对 imgKey 和 subKey 进行字符顺序打乱编码
    private static string GetMixinKey(string orig)
    {
        return MixinKeyEncTab.Aggregate("", (s, i) => s + orig[i])[..32];
    }

    private static Dictionary<string, string> EncWbi(Dictionary<string, string> parameters, string imgKey,
                                                     string                     subKey)
    {
        string mixinKey = GetMixinKey(imgKey + subKey);
        string currTime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        //添加 wts 字段
        parameters["wts"] = currTime;
        // 按照 key 重排参数
        parameters = parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
        //过滤 value 中的 "!'()*" 字符
        parameters = parameters.ToDictionary(
            kvp => kvp.Key,
            kvp => new string(kvp.Value.Where(chr => !"!'()*".Contains(chr)).ToArray())
        );
        // 序列化参数
        string query = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
        //计算 w_rid
        using MD5 md5       = MD5.Create();
        byte[]    hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query + mixinKey));
        string    wbiSign   = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        parameters["w_rid"] = wbiSign;

        return parameters;
    }

    // 获取最新的 img_key 和 sub_key
    public static async Task<(string, string)> GetWbiKeys()
    {
        HttpResponseMessage responseMessage = await Client.SendAsync(new HttpRequestMessage
                                                                          {
                                                                              Method = HttpMethod.Get,
                                                                              RequestUri =
                                                                                  new Uri(
                                                                                      "https://api.bilibili.com/x/web-interface/nav"),
                                                                          });

        JsonNode response = JsonNode.Parse(await responseMessage.Content.ReadAsStringAsync())!;

        string imgUrl = (string)response["data"]!["wbi_img"]!["img_url"]!;
        imgUrl = imgUrl.Split("/")[^1].Split(".")[0];

        string subUrl = (string)response["data"]!["wbi_img"]!["sub_url"]!;
        subUrl = subUrl.Split("/")[^1].Split(".")[0];
        return (imgUrl, subUrl);
    }

    public static async Task<(string, string)> GetWbiKeysFromDatabase()
    {
        var imgKeyConfig = DatabaseHandler.Instance.GetData<Config>(c => c.Key == "wbi_img_url");
        
        var lastUpdateTime = imgKeyConfig.LastTimeUpdate.ToDateTime();
        
        var timeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks - lastUpdateTime.Ticks);
        
        // check if last update time is 8 hour ago
        if (timeSpan.TotalHours> 8)
        {
            var data = await SendRequest("https://api.bilibili.com/x/web-interface/nav", HttpMethod.Get, new());
            
            var wbiConsumer = new WbiConsumer();
        
            wbiConsumer.Consume(data);
        }
        
        imgKeyConfig = DatabaseHandler.Instance.GetData<Config>(c => c.Key == "wbi_img_url");
        var subUrlConfig = DatabaseHandler.Instance.GetData<Config>(c => c.Key == "wbi_sub_url");
        
        return (imgKeyConfig!.Value, subUrlConfig!.Value);
    }

    public static async Task<string> GetWbiSign(Dictionary<string, string> parameters)
    {
        var (imgKey, subKey) = await GetWbiKeysFromDatabase();
        Dictionary<string, string> signedParams = EncWbi(parameters, imgKey, subKey);
        return await new FormUrlEncodedContent(signedParams).ReadAsStringAsync();
    }
    
    
    

    #endregion

    #region SendRequest

    // public static Queue<HttpRequestMessage> RequestMessages = new();

    public static async Task<JsonNode?> SendRequest(string url, HttpMethod method, Dictionary<string, string> parameters)
    {
        // var httpClient = Client;

        if (url.Contains("wbi"))
        {
            var wbiSign = await GetWbiSign(parameters);
            url = $"{url}?{wbiSign}";
        }
        else
        {
            url = $"{url}?{await new FormUrlEncodedContent(parameters).ReadAsStringAsync()}";
        }

        var responseMessage = await Client.SendAsync(new HttpRequestMessage
                                                     {
                                                         Method     = method,
                                                         RequestUri = new Uri(url),
                                                     });

        var content = await responseMessage.Content.ReadAsStringAsync();

        // check response status code
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            Log.Error($"Request Error - {responseMessage.StatusCode} - {url} - {content}");
            return null;
        }

        // check response content
        if (string.IsNullOrEmpty(content))
        {
            Log.Error($"Request Error - Empty Content - {url}");
            return null;
        }

        // check response json
        var json = JsonNode.Parse(content);

        if (json == null)
        {
            Log.Error($"Request Error - Invalid Json - {url} - {content}");
            return null;
        }

        // check json content
        var       code    = json["code"]!.GetValue<int>();
        var       message = json["message"]!.GetValue<string>();
        var data    = json["data"]!;
        
        if (code != 0)
        {
            Log.Error($"Request Error - {code} - {message} - {url}");
            return null;
        }
        
        // Console.WriteLine(url);

        return data;
    }

    #endregion

    #region User Information

    /// <summary>
    /// 获取用户主页信息
    /// 
    /// </summary>
    /// <param name="mid"></param>
    /// <returns></returns>
    public static async Task<JsonNode?> FetchUserInfo(long mid)
    {
        var url        = $"https://api.bilibili.com/x/space/wbi/acc/info";
        var parameters = new Dictionary<string, string>
                         {
                             { "mid", mid.ToString() },
                         };

        return await SendRequest(url, HttpMethod.Get, parameters);
    }

    /// <summary>
    /// 获取用户投稿视频信息
    /// </summary>
    /// <param name="mid"></param>
    /// <returns></returns>
    public static async Task<JsonNode?> FetchUserVideoInfo(long mid)
    {
        var url = "https://api.bilibili.com/x/space/wbi/arc/search";
        //
        var parameters = new Dictionary<string, string>()
                         {
                             { "mid", mid.ToString() },
                         };
        
        return await SendRequest(url, HttpMethod.Get, parameters);
    }

    #endregion

    #region Video Information

    /// <summary>
    /// 获取视频的详细信息
    /// </summary>
    /// <param name="bvid"></param>
    /// <returns></returns>
    public static async Task<JsonNode?> FetchVideoDetailInfo(string bvid)
    {
        // var httpClient = Client;
        //
        var url = "https://api.bilibili.com/x/web-interface/view";
        //
        var parameters = new Dictionary<string, string>()
        {
            { "bvid", bvid },
        };
        
        return await SendRequest(url, HttpMethod.Get, parameters);
        
        //
        // var uriString = $"{url}?bvid={bvid}";
        //
        // // Log.Warning(uriString);
        //
        // HttpResponseMessage responseMessage = await httpClient.SendAsync(new HttpRequestMessage
        //                                                                  {
        //                                                                      Method     = HttpMethod.Get,
        //                                                                      RequestUri = new Uri(uriString),
        //                                                                  });
        //
        // return JsonNode.Parse(await responseMessage.Content.ReadAsStringAsync())!;
    }

    #endregion
}