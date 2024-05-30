using System.Net;
using System.Text.Json.Nodes;
using MyBiliBiliMonitor_2;
using Lib;
using Lib.API;
using Lib.Tool;

namespace Test;

// [TestFixture]
public class UnitTest2
{
    [SetUp]
    public void SetUp()
    {
#if DEBUG
        RunTimeVarHelper.PathHome = RunTimeVarHelper.PathHome =
            @".."                       + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." +
            Path.DirectorySeparatorChar + ".."                        + Path.DirectorySeparatorChar;
        Log.Register(ConsoleAppLog.ConsolePrint);
#endif
    }

    [Test]
    public async Task TestFetchUserInfo()
    {
        var client = HttpClientProvider.GetHttpClient();
        
        var url = $"https://api.bilibili.com/x/web-interface/card";
        var parameters = new Dictionary<string, string>
                         {
                             { "mid", 3537123554626307.ToString() }, // testing user id who does not have any video uploaded
                         };
        
        var wbiSign = await APIHelper.GetWbiSign(parameters);
        url = $"{url}?{wbiSign}";
        
        Log.Info(url);

        var responseMessage = await client.SendAsync(new HttpRequestMessage
                                                     {
                                                         Method = HttpMethod.Get,
                                                         RequestUri = new Uri(url),
                                                     });

        Log.Info(responseMessage.StatusCode.ToString());
        
        var content = await responseMessage.Content.ReadAsStringAsync();
        
        var json = JsonNode.Parse(content);

        var code = json["code"];

        if (!(code != null && code.GetValue<int>() == 0))
        {
            Log.Error("Some thing wrong when fetching user info -- 01");
            
            return;
        }
        
        var data = json["data"];
        
        Log.Info(data);
        
        var consumer = new UserDataConsumer();
        
        consumer.Consume(data);
        
        // Log.Info();
    }
    
}