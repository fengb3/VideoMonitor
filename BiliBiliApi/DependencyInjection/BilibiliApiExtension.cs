using System.Net;
using System.Text.Json;
using BiliBiliApi.Apis.Auth;
using WebApiClientCore.Extensions.OAuths;
using BiliBiliApi.Apis;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class BilibiliApiExtension
{
	public static IServiceCollection AddBilibiliApi(this IServiceCollection services)
	{
		services
		   .AddHttpApi<ISignApi>()
			;

		services
		   .AddHttpApi<IBilibiliApi>()
		   .ConfigureHttpClient(options =>
			{
				options.DefaultRequestHeaders.Add("User-Agent",
				                                  "Mozilla/5.0 "                   +
				                                  "(Windows NT 10.0; Win64; x64) " +
				                                  "AppleWebKit/537.36 "            +
				                                  // "(KHTML, like Gecko) "           +
				                                  "Chrome/58.0.3029.110 "          +
				                                  "Safari/537.3");
				options.DefaultRequestHeaders.Add("Referer", "https://www.bilibili.com/");
				options.DefaultRequestHeaders.Add("Origin", "https://www.bilibili.com/");

				options.Timeout = TimeSpan.FromSeconds(10);
			})
		   .ConfigureHttpApi(options =>
			{
				options.KeyValueSerializeOptions.IgnoreNullValues = true;
				options.UseLogging                                = true;
			})
		   .ConfigurePrimaryHttpMessageHandler(() =>
			{
				var handler = new HttpClientHandler();
				handler.CookieContainer = new CookieContainer();

				handler.UseCookies = true;
				handler.CookieContainer.Add(
					new Uri("https://api.bilibili.com"),
					new Cookie("SESSDATA",
					           "c857904e%2C1732783007%2C3c12c%2A62CjBELchF7Bo8kb56-BHQNNZTX1ccPid2n8Tvikzg-aPXgh1L-Q1FXoIQ1vRcW0ZtOs4SVlBBS0hIRFZGamlvdWFPdzlscEZFQWVaNHE1Q3M2emFkbFphSUgzZnEwSWtOd1I4bUxaSmozQzQtamFhY29ad2NuTkxQSlR0RE1pd2VGaU1PVkhBYzNBIIEC"));

				handler.CookieContainer.Add(
					new Uri("https://api.bilibili.com"),
					new Cookie(
						"buvid3",
						"64382025-0AAB-8389-2EA8-0FDCCF00865699609infoc")
				);

				handler.CookieContainer.Add(
					new Uri("Https://api.bilibili.com"),
					new Cookie(
						"buvid4",
						"DAC8A899-9006-0900-5D29-5D64C5BB0F8824741-023013104-NLgo%2F2RgzmTINRwjdsii8w%3D%3D"
					)
				);

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

				Task.Delay(1000).Wait();

				return new TokenResult
				{
					Access_token = json,
					Expires_in = (long)TimeSpan.FromHours(8)
					                           .TotalSeconds
				};
			})
			;

		return services;
	}
}