using BiliBiliApi.Models;
using WebApiClientCore.Attributes;

namespace BiliBiliApi.Apis.Auth;

[HttpHost("https://api.bilibili.com")]
public interface ISignApi
{
    [HttpGet("/x/web-interface/nav")]
    Task<WbiResponse> GetNavAsync();
}