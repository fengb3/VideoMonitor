using System.Text.Json.Nodes;
using Model;
using MyLib.DataManagement;
using MyLib.Helper;
using MyLib.Tool;

namespace MyLib.API;

/// <summary>
/// 解析用户的名片信息
/// <see>
///     <cref>https://socialsisteryi.github.io/bilibili-API-collect/docs/user/info.html#%E7%94%A8%E6%88%B7%E5%90%8D%E7%89%87%E4%BF%A1%E6%81%AF</cref>
/// </see>
/// url: https://api.bilibili.com/x/web-interface/card
/// </summary>
public class UserDataConsumer : IDataConsumer<JsonNode>
{
    public void Consume(JsonNode? data)
    {
        if (data == null)
        {
            Log.Error($"解析用户信息失败，data为null");
            return;
        }

        var card = data["card"]; // card info

        if (card == null)
        {
            Log.Error($"解析用户信息失败，card为null");
            return;
        }

        var archiveCount = data["archive_count"]!.GetValue<int>(); // video count
        var follower     = data["follower"]!.GetValue<int>();      // follower count
        var likeNum      = data["like_num"]!.GetValue<int>();       // like count


        // data in card
        var mid            = card["mid"]!.GetValue<string>();    // user id (UID)
        var name           = card["name"]!.GetValue<string>(); // user's name
        var sex            = card["sex"]!.GetValue<string>();  // 男 女 保密
        var face           = card["face"]!.GetValue<string>(); // user's face url
        var fans           = card["fans"]!.GetValue<int>();
        var friend         = card["friend"]!.GetValue<int>();
        var attention      = card["attention"]!.GetValue<int>();
        var sign           = card["sign"]!.GetValue<string>();
        var levelInfo      = card["level_info"];
        var pendant        = card["pendant"];
        var nameplate      = card["nameplate"];
        var officialVerify = card["official_verify"];
        var vip            = card["vip"];
        var archiveView    = card["archive_view"];
        
        
        var timeStampNow = DateTime.Now.ToTimeStamp();

        var userRecord = new UserRecord()
                         {
                             Uid         = long.Parse(mid),
                             ArchiveNum  = archiveCount,
                             FollowerNum = follower,
                             FollowingNum = friend,
                             LikeNum     = likeNum,
                             TimeStamp = timeStampNow,
                         };
        
        DatabaseHandler.Instance.AddData(userRecord);

        if (userRecord.UrId == 0)
        {
            Log.Error($"添加用户记录到数据库失败 - {userRecord}");
        }
        
        var user = new User()
                   {
                       Uid                    = long.Parse(mid),
                       Name                   = name,
                       FaceUrl                = face,
                       MostRecentUserRecordId = userRecord.UrId,
                   };
        
        DatabaseHandler.Instance.UpdateData(user);
        
        Log.Info($"解析用户数据成功 - {user}");
        
    }
}