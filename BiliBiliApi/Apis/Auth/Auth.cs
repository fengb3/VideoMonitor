using BiliBiliApi.Dto;
using WebApiClientCore.Attributes;

namespace BiliBiliApi.Apis.Auth;

[HttpHost("https://api.bilibili.com")]
public interface ISignApi
{
    /// <summary>
    /// 获取签名
    /// </summary>
    /// <returns></returns>
    [HttpGet("/x/web-interface/nav")]
    Task<WbiResponse> GetNavAsync();
}