using System.Collections.Concurrent;
using System.Net;
using Lib.DataManagement;
using Model.RunTimeVar;

namespace Lib.API;

public static class HttpClientProvider
{
    private static ConcurrentQueue<HttpClient> HttpClients { get; } = new();

    private static CookieContainer _cookieContainer { get; } = new();

    static HttpClientProvider()
    {
        // _cookieContainer.Add(new Uri("https://api.bilibili.com"),
        //                      new Cookie("SESSDATA", 
        //                                 DatabaseHandler.Instance.GetData<Config>("SESSDATA").Value));
        //
        // _cookieContainer.Add(new Uri("https://api.bilibili.com"),
        //                      new Cookie(
        //                          "buvid3",
        //                          DatabaseHandler.Instance.GetData<Config>("buvid3").Value));
        //
        // _cookieContainer.Add(new Uri("https://api.bilibili.com"),
        //                      new Cookie(
        //                          "buvid4",
        //                          DatabaseHandler.Instance.GetData<Config>("buvid4").Value));
    }

    public static HttpClient GetHttpClient()
    {
        if (!HttpClients.TryDequeue(out var client))
        {
            // var cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler();
            handler.CookieContainer = _cookieContainer;

            client = new HttpClient(handler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"); // 浏览器的User-Agent

        }

        return client;
    }

    public static void ReturnHttpClient(HttpClient client)
    {
        HttpClients.Enqueue(client);
    }
}