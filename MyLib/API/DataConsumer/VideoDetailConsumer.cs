using System.Text.Json.Nodes;
using Model;
using MyLib.DataManagement;
using MyLib.Helper;
using MyLib.Tool;

namespace MyLib.API;

/// <summary>
/// 解析视频详情
/// <see>
///     <cref>https://socialsisteryi.github.io/bilibili-API-collect/docs/video/info.html#%E8%8E%B7%E5%8F%96%E8%A7%86%E9%A2%91%E8%AF%A6%E7%BB%86%E4%BF%A1%E6%81%AF-web%E7%AB%AF</cref>
/// </see>
/// url: https://api.bilibili.com/x/web-interface/view
/// </summary>
public class VideoDetailConsumer : IDataConsumer<JsonNode>
{
    public void Consume(JsonNode? data)
    {
        if (data == null)
        {
            Log.Error($"解析视频详情失败，data为null");
            return;
        }

        var aid         = data["aid"]!.GetValue<long>();            // 视频id
        var bvid        = data["bvid"]!.GetValue<string>();         // 视频bvid
        var videos      = data["videos"]!.GetValue<int>();          // 视频分p数量
        var tid         = data["tid"]!.GetValue<int>();             // 视频分区id
        var tname       = data["tname"]!.GetValue<string>();        // 视频分区名称
        var copyRight   = data["copyright"]!.GetValue<int>();       // 版权标识
        var pic         = data["pic"]!.GetValue<string>();          // 视频封面
        var title       = data["title"]!.GetValue<string>();        // 视频标题
        var pubdate     = data["pubdate"]!.GetValue<long>();        // 视频发布时间
        var ctime       = data["ctime"]!.GetValue<long>();          // 视频创建时间
        var desc        = data["desc"]!.GetValue<string>();         // 视频简介
        var descV2      = data["desc_v2"]!.GetValue<JsonNode>();    // 视频简介
        var state       = data["state"]!.GetValue<int>();           // 视频状态
        var duration    = data["duration"]!.GetValue<int>();        // 视频时长
        var forward     = data["forward"]!.GetValue<int>();         // 撞车视频跳转avid
        var missionId   = data["mission_id"]!.GetValue<int>();      // 稿件参加的活动id
        var redirectUrl = data["redirect_url"]?.GetValue<string>(); // 撞车视频跳转url 
        var rights      = data["rights"];                           // 视频权限标识
        var owner       = data["owner"];                            // 视频作者信息
        var stat        = data["stat"];                             // 视频状态信息
        var dynamic     = data["dynamic"];                          // 视频动态信息
        var cid         = data["cid"];                              // 视频cid
        var dimension   = data["dimension"];                        // 视频分辨率信息
        var staff       = data["staff"];                            // 视频制作人员信息
        var pages       = data["pages"];                            // 视频分p信息

        if (owner == null)
        {
            Log.Error($"解析视频详情失败，owner为null");
            return;
        }

        var mid  = owner["mid"]!.GetValue<long>();    // 作者id
        var name = owner["name"]!.GetValue<string>(); // 作者名称
        var face = owner["face"]!.GetValue<string>(); // 作者头像

        if (stat == null)
        {
            Log.Error($"解析视频详情失败，stat为null");
            return;
        }

        var view       = stat["view"]!.GetValue<int>();          // 播放量
        var danmaku    = stat["danmaku"]!.GetValue<int>();       // 弹幕数
        var reply      = stat["reply"]!.GetValue<int>();         // 评论数
        var favorite   = stat["favorite"]!.GetValue<int>();      // 收藏数
        var coin       = stat["coin"]!.GetValue<int>();          // 硬币数
        var share      = stat["share"]!.GetValue<int>();         // 分享数
        var now_rank   = stat["now_rank"]!.GetValue<int>();      // 当前排名
        var his_rank   = stat["his_rank"]!.GetValue<int>();      // 历史最高排名
        var like       = stat["like"]!.GetValue<int>();          // 点赞数
        var dislike    = stat["dislike"]!.GetValue<int>();       // 点踩数 always 0
        var evaluation = stat["evaluation"]!.GetValue<string>(); // 评分
        var argue_msg  = stat["argue_msg"]!.GetValue<string>();  // 警告 / 争议信息

        var videoRecord = new VideoRecord
                          {
                              BvId           = bvid,
                              TimeStamp      = DateTime.Now.ToTimeStamp(),
                              Likes          = like,
                              Dislikes       = dislike,
                              Coins          = coin,
                              Favorites      = favorite,
                              Shares         = share,
                              BulletComments = danmaku,
                              Comments       = reply,
                              Views          = view,
                          };
        
        DatabaseHandler.Instance.AddData(videoRecord);
        
        Log.Info($"解析视频统计信息成功 - {videoRecord.BvId} - {videoRecord.Views} - {videoRecord.Likes} - {videoRecord.Dislikes} - {videoRecord.Coins} - {videoRecord.Favorites} - {videoRecord.Shares} - {videoRecord.BulletComments} - {videoRecord.Comments}");
    }
}