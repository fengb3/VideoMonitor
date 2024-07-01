using System.Text.Json;
using BiliBiliApi.Dto;
using BiliBiliApi.Utils;
using WebApiClientCore;
using WebApiClientCore.Attributes;
using WebApiClientCore.Extensions.OAuths;

namespace BiliBiliApi.Attributes;

internal class WbiSignAttribute : OAuthTokenAttribute
{
    protected override void UseTokenResult(ApiRequestContext context, TokenResult tokenResult)
    {
        if (tokenResult.Access_token == null)
            throw new NullReferenceException("Wbi token is null");
        
        var wbiImg = JsonSerializer.Deserialize<WbiResponse.WbiImg>(tokenResult.Access_token);
        
        var imgKey = wbiImg?.ImgUrl?.Split("/")[^1].Split(".")[0];
        var subKey = wbiImg?.SubUrl?.Split("/")[^1].Split(".")[0];

        var query = context.HttpContext.RequestMessage.RequestUri?.ToString().Split("?")[^1];

        var queryDict = query
                       .Split("&")
                             .Select(x => x.Split("="))
                             .ToDictionary(x => x[0], x => x[1]);

        var signedParams = WbiSignature.EncWbi(queryDict, imgKey, subKey);
        var signedQuery  = new FormUrlEncodedContent(signedParams).ReadAsStringAsync().Result;

        context.HttpContext.RequestMessage.RequestUri =
            new Uri(context.HttpContext.RequestMessage.RequestUri?.ToString().Split('?')[0] + "?" + signedQuery);
    }
}