using BiliBiliApi.Attributes;
using BiliBiliApi.Models.Space.Arc;
using WebApiClientCore.Attributes;

namespace BiliBiliApi.Apis.Space;

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
	Task<Search.Response> SpaceArcSearch([PathQuery] long   mid,
	                                     [PathQuery] string order,
	                                     [PathQuery] int    tid,
	                                     [PathQuery] string keyword,
	                                     [PathQuery] int    pn,
	                                     [PathQuery] int    ps);
}

// [HttpHost("https://api.bilibili.com")]
public interface IAccApi
{
    /// <summary>
    /// 用户空间详细信息
    /// </summary>
    /// <param name="mid">目标用户mid</param>
    /// <returns></returns>
    [HttpGet("/x/space/wbi/acc/info")]
	[WbiSign]
	Task<object> SpaceAccInfo([PathQuery] int mid);
}