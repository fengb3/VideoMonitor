using System.Runtime.InteropServices;
using BiliBiliApi.Attributes;
using WebApiClientCore.Attributes;

namespace BiliBiliApi.Apis;

/// <summary>
/// b站接口
/// </summary>
[HttpHost("https://api.bilibili.com")]
public interface IBilibiliApi : Space.Space, Space.IArcApi, Space.IAccApi;