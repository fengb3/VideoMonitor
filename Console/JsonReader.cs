using System.Text.Json;
using Lib;
using Lib.Helper;
using Lib.Tool;
using Model;
using System.Collections.Generic;
using Model.DataManage;

namespace Console;

public static class JsonReader
{
    public static void Read()
    {
        // 从 config 中获取 所有的 uid 
        var uidJsonElement = Config.Get("uids");

        var uids = uidJsonElement?.EnumerateArray().Select(uid => uid.GetInt64()).ToList();

        uids?.ForEach(ReadUser);
    }

    /// <summary>
    /// 读取用户信息
    /// 包括用户的 视频
    /// 并写入数据库
    /// </summary>
    /// <param name="uid"></param>
    private static void ReadUser(long uid)
    {
        Log.Debug($"开始读取用户{uid}的数据");

        // 路径检查
        var pathToUserData = Path.Combine(Config.PathHome, Config.Get("path_data").ToString() ?? "-1", uid.ToString());

        if (!Directory.Exists(pathToUserData))
        {
            Log.Error($"Path to user data{pathToUserData} not found, can't read user data");
            return;
        }

        var pathToUserInfo = Path.Combine(pathToUserData, Config.Get("path_user_info").ToString() ?? "-1");

        if (!Directory.Exists(pathToUserInfo))
        {
            Log.Error($"Path to user info {pathToUserInfo} not found, can't read user info");
            return;
        }

        // 遍历 pathToUserInfo 下的所有文件
        // 不包含 .read 文件
        var files = Directory.GetFiles(pathToUserInfo).Where(f => !f.EndsWith(@".read")).ToList();

        if (files.Count == 0)
        {
            Log.Warning($"No unread file found in {pathToUserInfo} , skip");
            return;
        }

        var u = new User()
        {
            Uid = uid
        };

        var records        = new List<UserRecord>();
        var bvids          = new HashSet<string>();
        var mostUpdateTime = new DateTime();

        foreach (var file in files)
        {
            if (file.Split('.').Last() == @"read")
                continue;

            Log.Debug("开始读取用户记录文件" + file);

            // 文件名 获取时间戳
            var timeStampString = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
            var recordTime      = timeStampString.ToDateTime();

            // 文件内容 获取用户当前时间的信息
            var json        = File.ReadAllText(file);
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);

            var userInfo         = jsonElement.GetProperty(@"user_info");
            var userRelationInfo = jsonElement.GetProperty(@"relation_info");
            var userBvids        = jsonElement.GetProperty(@"bvids");

            var username = userInfo.GetProperty(@"name").ToString(); // 用户名
            var faceUrl  = userInfo.GetProperty(@"face").ToString(); // 头像

            var followerNumber   = userRelationInfo.GetProperty(@"follower");  // 粉丝数
            var followingNumber  = userRelationInfo.GetProperty(@"following"); // 关注数
            var bvidsJsonElement = userBvids.EnumerateArray();                 // 视频列表

            // 添加视频列表到set 去重
            foreach (var bvid in bvidsJsonElement)
            {
                var bviIdStr = bvid.GetString();
                if (bviIdStr != null)
                    bvids.Add(bviIdStr);
            }

            // 更新最新时间 的用户名 和 头像
            if (DateTime.Compare(mostUpdateTime, recordTime) < 0)
            {
                mostUpdateTime = recordTime;
                u.Name         = username;
                u.FaceUrl      = faceUrl;
            }

            // 添加记录
            var ur = new UserRecord()
            {
                Uid          = uid,
                TimeStamp    = long.Parse(timeStampString),
                FollowerNum  = followerNumber.GetInt32(),
                FollowingNum = followingNumber.GetInt32(),
            };

            records.Add(ur);

            Log.Debug($"读取了用户 {uid} - {username} 在 {ur.TimeStamp.ToDateTime()} 的记录文件 {file}");
        }

        Log.Debug($"开始将用户 {uid} - {u.Name} 的数据写入数据库");

        // 添加用户信息
        // MAYBE 不去遍历数据
        if (DatabaseHandler.Instance.GetData<User>().Any(user => user.Uid == uid))
        {
            Log.Debug($"更新用户 {uid} - {u.Name} 的信息 到数据库");
            DatabaseHandler.Instance.UpdateData(u);
        }
        else
        {
            Log.Debug($"添加用户 {uid} - {u.Name} 的信息 到数据库");
            DatabaseHandler.Instance.AddData(u);
        }

        // 添加记录
        foreach (var record in records)
        {
            Log.Debug(
                $"添加 用户 {record.Uid} 在 {record.TimeStamp.ToDateTime()} 的记录 到数据库 - 粉丝数 {record.FollowerNum} 关注数 {record.FollowingNum}");
            DatabaseHandler.Instance.AddData(record);
        }

        // 读取视频信息
        foreach (var bvid in bvids)
        {
            ReadVideo(uid, bvid, pathToUserData);
        }

        // 删除文件夹 下的记录文件
        foreach (var file in files)
        {
            Log.Debug($"重命名文件 {file} + .read");
            File.Move(file, file + @".read");
        }
    }

    /// <summary>
    /// 读取视频信息
    /// 并写入数据库
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="bvid"></param>
    /// <param name="pathToUserData"></param>
    private static void ReadVideo(long uid, string bvid, string pathToUserData)
    {
        Log.Debug($"开始读取用户 {uid} 的视频 {bvid} 的数据");

        var pathToVideoInfo = Path.Combine(pathToUserData, bvid);

        if (!Directory.Exists(pathToVideoInfo))
        {
            Log.Error($"Path to video info {pathToVideoInfo} not found, can't read video info");
            return;
        }

        // 遍历 pathToVideoInfo 下的所有文件
        // 不包含 .read 后缀的文件
        var files = Directory.GetFiles(pathToVideoInfo).Where(f => !f.EndsWith(@".read")).ToList();

        if (files.Count == 0)
        {
            Log.Warning($"No No unread file found in {pathToVideoInfo} , skip");
            return;
        }

        var v = new Video()
        {
            BvId     = bvid,
            AuthorId = uid
        };

        var records        = new List<VideoRecord>();
        var mostUpdateTime = new DateTime();

        foreach (var file in files)
        {
            Log.Debug($"开始读取视频 {bvid} 的记录文件 {file} ");

            // 文件名 获取时间戳
            var timeStampString = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
            var recordTime      = timeStampString.ToDateTime();

            // 文件内容 获取用户当前时间的信息
            var json        = File.ReadAllText(file);
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);

            var videoStat = jsonElement.GetProperty(@"stat");
            var owner = jsonElement.GetProperty(@"owner");
            
            var ownerId = owner.GetProperty(@"mid").GetInt64();
            
            if(ownerId != uid)
                Log.Warning($"视频 {bvid} 的作者 {ownerId} 与用户 {uid} 不一致");
            
            var tid = jsonElement.GetProperty(@"tid").GetInt32();
            var tname = jsonElement.GetProperty(@"tname").GetString();

            if (DatabaseHandler.Instance.GetData<VideoType>().All(t => t.TId != tid))
            {
                Log.Debug($"添加视频类型 {tid} - {tname} 到数据库");
                DatabaseHandler.Instance.AddData(new VideoType()
                {
                    TId   = tid,
                    Name = tname ?? "unknown"
                });
            }

            var title       = jsonElement.GetProperty(@"title").ToString();   // 标题
            var description = jsonElement.GetProperty(@"desc").ToString();    // 描述
            var coverUrl    = jsonElement.GetProperty(@"pic").ToString();     // 封面
            var pubDate     = jsonElement.GetProperty(@"pubdate").ToString(); // 发布时间

            var view     = videoStat.GetProperty(@"view");     // 播放数
            var danmaku  = videoStat.GetProperty(@"danmaku");  // 弹幕数
            var reply    = videoStat.GetProperty(@"reply");    // 评论数
            var favorite = videoStat.GetProperty(@"favorite"); // 收藏数
            var coin     = videoStat.GetProperty(@"coin");     // 投币数
            var share    = videoStat.GetProperty(@"share");    // 分享数
            var like     = videoStat.GetProperty(@"like");     // 点赞数
            var dislike  = videoStat.GetProperty(@"dislike");  // 点踩数

            // 更新最新时间 的标题 和 封面
            if (DateTime.Compare(mostUpdateTime, recordTime) < 0)
            {
                mostUpdateTime    = recordTime;
                v.Title           = title;
                v.Description     = description;
                v.CoverUrl        = coverUrl;
                v.UploadTimeStamp = long.Parse(pubDate);
            }

            var vr = new VideoRecord()
            {
                BvId           = bvid,
                TimeStamp      = long.Parse(timeStampString),
                Views          = view.GetInt32(),
                BulletComments = danmaku.GetInt32(),
                Comments       = reply.GetInt32(),
                Favorites      = favorite.GetInt32(),
                Coins          = coin.GetInt32(),
                Shares         = share.GetInt32(),
                Likes          = like.GetInt32(),
                Dislikes       = dislike.GetInt32(),
            };

            records.Add(vr);

            Log.Debug($"读取了视频 {bvid} 在 {vr.TimeStamp.ToDateTime()} 的记录文件{file}");
        }

        Log.Debug($"开始将视频 {v.BvId} - {v.Title} 的数据写入数据库");

        // 添加视频信息
        // MAYBE 不去遍历数据
        if (DatabaseHandler.Instance.GetData<Video>().Any(video => video.BvId == bvid))
        {
            Log.Debug($"更新视频 {v.BvId} - {v.Title} 的信息 到数据库");
            DatabaseHandler.Instance.UpdateData(v);
        }
        else
        {
            Log.Debug($"添加视频 {v.BvId} - {v.Title} 的信息 到数据库");
            DatabaseHandler.Instance.AddData(v);
        }

        // 添加记录
        foreach (var record in records)
        {
            Log.Debug($"添加视频 {v.BvId} - {v.Title} 在 {record.TimeStamp.ToDateTime()} 的记录 到数据库 " +
                      $"播放 {record.Views} 弹幕 {record.BulletComments} 评论 {record.Comments} 收藏 {record.Favorites} 投币 {record.Coins} 分享 {record.Shares} 点赞 {record.Likes} 点踩 {record.Dislikes}");
            DatabaseHandler.Instance.AddData(record);
        }

        // 删除文件夹 下的记录文件
        foreach (var file in files)
        {
            Log.Debug($"重命名文件 {file} + .read");
            File.Move(file, file + @".read");
        }
    }
}