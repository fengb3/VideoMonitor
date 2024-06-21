using System.Net;
using System.Text.Json;
using BiliBiliApi.Apis.Auth;
using WebApiClientCore.Extensions.OAuths;
using BiliBiliApi.Apis;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class BilibiliApiExtension
{
    /// <summary>
    /// ��� BilibiliApiSolid ����
    /// ��Ӳ�棬 TODO: Remove it before push
    /// </summary>
    /// <param name="services">���񼯺�</param>
    /// <returns>���񼯺�</returns>
    public static IServiceCollection AddBilibiliApiSolid(this IServiceCollection services)
    {
        return services.AddBilibiliApi(option =>
        {
            option.Cookies =
            [
                new("SESSDATA", "c857904e%2C1732783007%2C3c12c%2A62CjBELchF7Bo8kb56-BHQNNZTX1ccPid2n8Tvikzg-aPXgh1L-Q1FXoIQ1vRcW0ZtOs4SVlBBS0hIRFZGamlvdWFPdzlscEZFQWVaNHE1Q3M2emFkbFphSUgzZnEwSWtOd1I4bUxaSmozQzQtamFhY29ad2NuTkxQSlR0RE1pd2VGaU1PVkhBYzNBIIEC"),
                new("buvid3","64382025-0AAB-8389-2EA8-0FDCCF00865699609infoc"),
                new("buvid4", "DAC8A899-9006-0900-5D29-5D64C5BB0F8824741-023013104-NLgo%2F2RgzmTINRwjdsii8w%3D%3D")
            ];
        });

        //services
        //   .AddHttpApi<ISignApi>()
        //    ;
        //services
        //   .AddHttpApi<IBilibiliApi>()
        //   .ConfigureHttpClient(options =>
        //    {
        //        options.DefaultRequestHeaders.Add("User-Agent",
        //                                          "Mozilla/5.0 " +
        //                                          "(Windows NT 10.0; Win64; x64) " +
        //                                          "AppleWebKit/537.36 " +
        //                                          // "(KHTML, like Gecko) "           +
        //                                          "Chrome/58.0.3029.110 " +
        //                                          "Safari/537.3");
        //        options.DefaultRequestHeaders.Add("Referer", "https://www.bilibili.com/");
        //        options.DefaultRequestHeaders.Add("Origin", "https://www.bilibili.com/");

        //        options.Timeout = TimeSpan.FromSeconds(10);
        //    })
        //   .ConfigureHttpApi(options =>
        //    {
        //        options.KeyValueSerializeOptions.IgnoreNullValues = true;
        //        options.UseLogging = true;
        //    })
        //   .ConfigurePrimaryHttpMessageHandler(() =>
        //    {
        //        var handler = new HttpClientHandler();
        //        handler.CookieContainer = new CookieContainer();

        //        handler.UseCookies = true;
        //        handler.CookieContainer.Add(
        //            new Uri("https://api.bilibili.com"),
        //            new Cookie("SESSDATA",
        //                       "c857904e%2C1732783007%2C3c12c%2A62CjBELchF7Bo8kb56-BHQNNZTX1ccPid2n8Tvikzg-aPXgh1L-Q1FXoIQ1vRcW0ZtOs4SVlBBS0hIRFZGamlvdWFPdzlscEZFQWVaNHE1Q3M2emFkbFphSUgzZnEwSWtOd1I4bUxaSmozQzQtamFhY29ad2NuTkxQSlR0RE1pd2VGaU1PVkhBYzNBIIEC"));

        //        handler.CookieContainer.Add(
        //            new Uri("https://api.bilibili.com"),
        //            new Cookie(
        //                "buvid3",
        //                "64382025-0AAB-8389-2EA8-0FDCCF00865699609infoc")
        //        );

        //        handler.CookieContainer.Add(
        //            new Uri("Https://api.bilibili.com"),
        //            new Cookie(
        //                "buvid4",
        //                "DAC8A899-9006-0900-5D29-5D64C5BB0F8824741-023013104-NLgo%2F2RgzmTINRwjdsii8w%3D%3D"
        //            )
        //        );

        //        return handler;
        //    })
        //    ;


        //services
        //   .AddTokenProvider<IBilibiliApi>(async provider =>
        //    {
        //        var wbi = await provider
        //                       .GetRequiredService<ISignApi>()
        //                       .GetNavAsync()
        //            ;

        //        if (wbi.Code != 0 && wbi.Code != -101)
        //        {
        //            throw new Exception(wbi.Message);
        //        }

        //        var json = JsonSerializer.Serialize(wbi.Data?.WbiImg);

        //        Task.Delay(1000).Wait(); // WHY I WAIT FOR 1 SECOND? TODO: Figure out why ?

        //        return new TokenResult
        //        {
        //            Access_token = json,
        //            Expires_in = (long)TimeSpan.FromHours(8)
        //                                       .TotalSeconds
        //        };
        //    })
        //    ;
        //return services;
    }

    /// <summary>
    /// ��� BilibiliApi ����
    /// </summary>
    /// <param name="services">���񼯺�</param>
    /// <param name="setupAction">����ѡ��</param>
    /// <returns>���񼯺�</returns>
    /// <exception cref="Exception">����ȡ������Ϣʧ��ʱ�׳��쳣</exception>
    public static IServiceCollection AddBilibiliApi(this IServiceCollection services, Action<BilibiliApiOptions> setupAction)
    {
        // ����BilibiliApiѡ��
        services.Configure(setupAction);

        services
           .AddHttpApi<ISignApi>()
            ;

        services
           .AddHttpApi<IBilibiliApi>()
           .ConfigureHttpClient(options =>
           {
               options.DefaultRequestHeaders.Add("User-Agent", BilibiliApiOptions.UserAgent);
               options.DefaultRequestHeaders.Add("Referer", BilibiliApiOptions.Referer);
               options.DefaultRequestHeaders.Add("Origin", BilibiliApiOptions.Origin);

               options.Timeout = TimeSpan.FromSeconds(10);
           })
           .ConfigureHttpApi(options =>
           {
               options.KeyValueSerializeOptions.IgnoreNullValues = true;
               options.UseLogging = true;
           })
           .ConfigurePrimaryHttpMessageHandler(provider =>
           {
               var options = provider.GetRequiredService<IOptions<BilibiliApiOptions>>().Value;

               var handler = new HttpClientHandler();
               if (handler.SupportsAutomaticDecompression)
               {
                   handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
               }
               handler.UseCookies = true;
               handler.CookieContainer = new System.Net.CookieContainer();
               foreach (var cookie in options.Cookies)
               {
                   handler.CookieContainer.Add(new Uri(options.BaseAddress), new System.Net.Cookie(cookie.Name, cookie.Value));
               }
               return handler;
           })
        ;

        services
           .AddTokenProvider<IBilibiliApi>(async provider =>
           {
               var wbi = await provider
                                      .GetRequiredService<ISignApi>()
                                      .GetNavAsync()
                           ;

               if (wbi.Code != 0 && wbi.Code != -101)
               {
                   throw new Exception(wbi.Message);
               }

               var json = JsonSerializer.Serialize(wbi.Data?.WbiImg);

               //Task.Delay(1000).Wait(); // WHY I WAIT FOR 1 SECOND? TODO: Figure out why ?

               return new TokenResult
               {
                   Access_token = json,
                   Expires_in = (long)TimeSpan.FromHours(8).TotalSeconds // TODO: Find out when to refresh token
               };
           })
            ;

        return services;

    }
}

/// <summary>
/// Bilibili API ����ѡ��
/// </summary>
public class BilibiliApiOptions
{
    /// <summary>
    /// Bվ API ����
    /// </summary>
    public string BaseAddress { get; init; } = "https://api.bilibili.com";

    /// <summary>
    /// Referer
    /// </summary>
    public const string Referer = "https://www.bilibili.com/";

    /// <summary>
    /// Origin
    /// </summary>
    public const string Origin = "https://www.bilibili.com/";

    /// <summary>
    /// User-Agent
    /// </summary>
    public const string UserAgent = "Mozilla/5.0 " +
                                    "(Windows NT 10.0; Win64; x64) " +
                                    "AppleWebKit/537.36 " +
                                    "Chrome/58.0.3029.110 " +
                                    "Safari/537.3";


    /// <summary>
    /// ���õ� Cookies
    /// </summary>
    /// <remarks>��Ҫ���� SESSDATA, buvid3, buvid4</remarks>
    public BiliBiliCookie[] Cookies { get; set; } = [];
}

/// <summary>
/// Bilibili Cookie
/// </summary>
public class BiliBiliCookie(string name, string value)
{
    /// <summary>
    /// Cookie ����
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Cookie ֵ
    /// </summary>
    public string Value { get; set; } = value;
    
}
