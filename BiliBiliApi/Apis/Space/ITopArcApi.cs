using BiliBiliApi.Attributes;
using WebApiClientCore.Attributes;

namespace BiliBiliApi.Space;

// [HttpHost("https://api.bilibili.com")]
public interface ITopArcApi
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
    /// <summary>
    /// 查询用户投稿视频明细
    /// </summary>
    /// <param name="mid"></param>
    /// <param name="order"></param>
    /// <param name="tid"></param>
    /// <param name="keyword"></param>
    /// <param name="pn"></param>
    /// <param name="ps"></param>
    /// <returns></returns>
    [HttpGet("/x/space/wbi/arc/search")]
    [WbiSign]
    Task<Arc.Search.Response> SpaceArcSearch([PathQuery] long   mid,
                                             [PathQuery] string order   = null,
                                             [PathQuery] int?   tid     = null,
                                             [PathQuery] string keyword = null,
                                             [PathQuery] int?   pn      = null,
                                             [PathQuery] int?   ps      = null);
}

// [HttpHost("https://api.bilibili.com")]
public interface IAccApi
{
    /// <summary>
    /// 用户空间详细信息
    /// </summary>
    /// <param name="mid">目标用户 mid</param>
    /// <returns></returns>
    [HttpGet("/x/space/wbi/acc/info")]
    [WbiSign]
    Task<Acc.Info.Response> SpaceAccInfo([PathQuery] int mid);
}