using BiliBiliApi.Attributes;
using WebApiClientCore.Attributes;

namespace BiliBiliApi.Apis.Space;

// [HttpHost("https://api.bilibili.com")]
public interface Space
{
    [HttpGet("/x/space/top/arc")]
    Task<string> SpaceTopArc([PathQuery] int vmid);

    [HttpPost("/x/space/top/arc/set")]
    Task<object> SpaceTopArcSet([PathQuery] int    aid,
                                [PathQuery] string bvid,
                                [PathQuery] string reason,
                                [PathQuery] string csrf);
    
    [HttpPost("/x/space/top/arc/del")]
    Task<object> SpaceTopArcDel([PathQuery] int    aid,
                                [PathQuery] string bvid,
                                [PathQuery] string csrf);
}

// [HttpHost("https://api.bilibili.com")]
public interface IArcApi
{
    [HttpGet("/x/space/wbi/arc/search")]
    [WbiSign]
    Task<Models.Space.Arc.Search.Response> SpaceArcSearch(Models.Space.Arc.Search.Request request);
}

// [HttpHost("https://api.bilibili.com")]
public interface IAccApi
{
    [HttpGet("/x/space/wbi/acc/info")]
    [WbiSign]
    Task<object> SpaceAccInfo([PathQuery] int mid);
}