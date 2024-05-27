using System.Text.Json.Nodes;
using Model;
using MyLib.DataManagement;
using MyLib.Tool;

namespace MyLib.API;

/// <summary>
/// 解析用户投稿明细
/// <see>
///     <cref>https://socialsisteryi.github.io/bilibili-API-collect/docs/user/space.html#%E6%9F%A5%E8%AF%A2%E7%94%A8%E6%88%B7%E6%8A%95%E7%A8%BF%E8%A7%86%E9%A2%91%E6%98%8E%E7%BB%86</cref>
/// </see>
/// url: https://api.bilibili.com/x/space/wbi/arc/search
/// </summary>
public class UserVideoConsumer : IDataConsumer<JsonNode>
{
    public void Consume(JsonNode? data)
    {
        if (data == null)
        {
            Log.Error($"解析用户投稿明细失败，data为null");
            return;
        }

        var list = data["list"];

        if (list == null)
        {
            Log.Error($"解析用户投稿明细失败，list为null");
            return;
        }

        var tlist = list["tlist"]; // 投稿视频分区索引	

        var vlist = list["vlist"]; // 投稿视频列表

        if (tlist is not JsonArray tArr)
        {
            Log.Error($"解析用户投稿分区明细失败，tlist 不为 JsonArray");
        }
        else
        {
            foreach (var node in tArr)
            {
                if (node == null) continue;

                var tCount = node["count"]!.GetValue<int>();   // 该分区视频数量
                var name   = node["name"]!.GetValue<string>(); // 分区名称
                var tid    = node["tid"]!.GetValue<int>();     // 分区id

                Log.Info($"分区 {name} - {tid} - {tCount}");
            }
        }

        if (vlist is not JsonArray vArr)
        {
            Log.Error($"解析用户投稿明细失败，vlist 不为 JsonArray");
        }
        else
        {
            foreach (var node in vArr)
            {
                if (node == null) continue;

                var aid         = node["aid"]!.GetValue<long>();           // 视频id
                var attribute   = node["attribute"]!.GetValue<long>();     // 
                var author      = node["author"]!.GetValue<string>();      // 作者 不一定为目标用户 (合作视频
                var bvid        = node["bvid"]!.GetValue<string>();        // bvid
                var comment     = node["comment"]!.GetValue<int>();        // 评论数
                var created     = node["created"]!.GetValue<long>();       // 上传时间
                var copyRight   = node["copyright"]!.GetValue<int>();      // 版权标识
                var description = node["description"]!.GetValue<string>(); // 视频简介
                var length      = node["length"]!.GetValue<string>();      // 视频时长 MM:SS
                var mid         = node["mid"]!.GetValue<long>();           // 作者id 不一定为目标用户 (合作视频
                // var meta        = node["meta"]; // 不明 或许为视频合作信息
                var pic         = node["pic"]!.GetValue<string>();       // 封面图片url
                var play        = node["play"]!.GetValue<int>();         // 播放数
                var review      = node["review"]!.GetValue<int>();       // 不明
                var subtitle    = node["subtitle"]!.GetValue<int>();     // 字幕标识
                var title       = node["title"]!.GetValue<string>();     // 视频标题
                var typeid      = node["typeid"]!.GetValue<int>();       // 分区id
                var videoReview = node["video_review"]!.GetValue<int>(); // 弹幕数量

                // var video = new Video()
                //             {
                //                 BvId            = bvid,
                //                 AuthorId        = mid,
                //                 TId             = typeid,
                //                 Title           = title,
                //                 Description     = description,
                //                 UploadTimeStamp = created,
                //                 CoverUrl        = pic,
                //             };

                if (DatabaseHandler.Instance.TryGetData(v => v.BvId == bvid, out Video video))
                {
                    video.AuthorId        = mid;
                    video.TId             = typeid;
                    video.Title           = title;
                    video.Description     = description;
                    video.UploadTimeStamp = created;
                    video.CoverUrl        = pic;

                    DatabaseHandler.Instance.UpdateData(video);
                    Log.Info($"更新了视频基本信息 for {video.BvId} - {video.Title}");
                }
                else
                {
                    video = new Video()
                            {
                                BvId            = bvid,
                                AuthorId        = mid,
                                TId             = typeid,
                                Title           = title,
                                Description     = description,
                                UploadTimeStamp = created,
                                CoverUrl        = pic,
                            };

                    DatabaseHandler.Instance.AddData(video);
                    Log.Info($"添加了视频基本信息 for {video.BvId} - {video.Title}");
                }
            }
        }

        var page = data["page"]; // 分页信息

        if (page == null)
        {
            Log.Error($"解析用户投稿明细失败，page为null");
            return;
        }

        // 分页信息 TODO: 根据分页信息继续发送请求获取更多数据
        var pageCount = page["count"]!.GetValue<int>(); // 总视频数
        var pageNum   = page["num"]!.GetValue<int>();   // 当前页视频数
        var pageSize  = page["size"]!.GetValue<int>();  // 每页视频数

        // 
        var episodicButton = data["episodic_button"]; //

        if (episodicButton == null)
        {
            Log.Warning($"解析用户投稿明细错误，episodic_button为null");
        }

        var text = episodicButton?["text"]!.GetValue<string>(); // 按钮文本
        var uri  = episodicButton?["uri"]!.GetValue<string>();  // 按钮链接

        Log.Info($"解析用户投稿明细中第{pageNum}页中的{pageSize}个视频成功");
    }
}