using WebApiClientCore.Attributes;

namespace BiliBiliApi.Web_Interface;

public interface IWebInterfaceApi
{
    /// <summary>
    /// 用户名片信息
    /// </summary>
    /// <param name="mid">目标用户mid</param>
    /// <param name="photo">是否请求用户主页头图</param>
    [HttpGet("/x/web-interface/card")]
    Task<Card.Response> WebInterfaceCard([PathQuery] long   mid,
                                         [PathQuery] string photo = "false");

    /// <summary>
    /// 获取视频详细信息(web端)
    /// <para>限制游客访问的视频需要登录</para>
    /// </summary>
    /// <param name="avid"></param>
    /// <param name="bvid"></param>
    /// <returns></returns>
    [HttpGet("/x/web-interface/view")]
    Task<View.Response> WebInterfaceView([PathQuery] long?   avid = null,
                                         [PathQuery] string? bvid = null);
}