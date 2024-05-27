using System.Text.Json.Nodes;
using MyLib.DataManagement;
using MyLib.Helper;
using MyLib.Tool;
using Model;
using MyBiliBiliMonitor_2;

namespace WebApp.TimeTask;

public class TaskTimer
{
    public readonly Timer      Timer;
    public readonly Func<Task> TaskAsync;
    public readonly string     Name;

    public TaskTimer(Func<Task> taskAsync, TimeSpan interval, string name)
    {
        TaskAsync = taskAsync;
        Name      = name;
        Timer     = new Timer(TimerCallback, null, TimeSpan.Zero, interval);
    }

    private async void TimerCallback(object? _)
    {
        Log.Info($"开始执行定时任务 - {Name}");
        await TaskAsync();
    }

    public void UpdateInterval(TimeSpan newInterval)
    {
        Timer.Change(TimeSpan.Zero, newInterval);
    }
}

public class TaskTimerManager : Dictionary<string, TaskTimer>
{
    public static TaskTimerManager Instance
    {
        get
        {
            _lazy ??= new Lazy<TaskTimerManager>(() => new());

            return _lazy.Value;
        }
    }

    private static Lazy<TaskTimerManager>? _lazy;
}

public static class TaskTimerExtension
{
    public static void SetUp()
    {
        TaskTimerManager.Instance.Add("Update_Users",
                                      new TaskTimer(UpdateUser, TimeSpan.FromDays(7), "更新全部用户信息"));
        TaskTimerManager.Instance.Add("Update_Videos",
                                      new TaskTimer(UpdateVideo, TimeSpan.FromHours(1), "更新全部视频信息"));
    }

    private static async Task UpdateUser()
    {
        var users = DatabaseHandler.Instance.GetAllData<User>();

        var usersToUpdate = new List<User>();

        Log.Info($"开始更新 {users.Count()} 个用户基本信息");

        foreach (var user in users)
        {
            // Log.Info($"开始更新 {user.Name} 的用户基本信息");

            var userInfo = await APIHelper.FetchUserInfo(user.Uid);

            if (userInfo == null)
            {
                Log.Error($"获取用户信息失败 - {user.Uid} - {user.Name}");
                continue;
            }

            usersToUpdate.Add(new User()
                              {
                                  Uid     = user.Uid,
                                  Name    = userInfo["name"]!.GetValue<string>(),
                                  FaceUrl = userInfo["face"]!.GetValue<string>(),
                              });

            var userVideoInfo = await APIHelper.FetchUserVideoInfo(user.Uid);

            if (userVideoInfo == null)
            {
                Log.Error($"获取用户视频信息失败 - {user.Uid} - {user.Name}");
                continue;
            }

            var vList = userVideoInfo["list"]!["vlist"]!;

            if (vList is not JsonArray vArr) continue;

            foreach (var node in vArr)
            {
                var bvid = node["bvid"].GetValue<string>();

                var video = new Video
                            {
                                BvId            = bvid,
                                AuthorId        = user.Uid,
                                Title           = node["title"]!.GetValue<string>(),
                                Description     = node["description"]!.GetValue<string>(),
                                UploadTimeStamp = node["created"]!.GetValue<long>(),
                                CoverUrl        = node["pic"]!.GetValue<string>(),
                                TId             = node["typeid"]!.GetValue<int>(),
                            };

                var videoExists = DatabaseHandler.Instance.GetAllData<Video>().Any(v => v.BvId == bvid);

                if (videoExists)
                {
                    DatabaseHandler.Instance.UpdateData(video);
                    Log.Info($"更新了视频基本信息 for {user.Name} - {video.BvId} - {video.Title} ");
                }
                else
                {
                    DatabaseHandler.Instance.AddData(video);
                    Log.Info($"添加了新的视频基本信息 for {user.Name} - {video.BvId} - {video.Title}");
                }
            }


            // 每次请求每个用户信息间隔1.5s
            await Task.Delay(1500);
        }

        foreach (var user in usersToUpdate)
        {
            DatabaseHandler.Instance.UpdateData(user);
            Log.Info($"更新了用户信息完成 - {user.Name}");
        }

        Log.Info($"更新全部用户信息完成");
    }

    private static async Task UpdateVideo()
    {
        var videos = DatabaseHandler.Instance.GetAllData<Video>();

        Log.Info($"开始更新 {videos.Count()} 个视频数据");

        foreach (var video in videos)
        {
            // Log.Info($"{video.BvId} - {video.Title} 开始更新视频统计信息");

            var videoInfo = await APIHelper.FetchVideoDetailInfo(video.BvId);

            if (videoInfo == null)
            {
                Log.Error($"获取视频信息失败 - {video.BvId} - {video.Title}");
                continue;
            }

            var bvid = videoInfo["bvid"]!.GetValue<string>();

            if (DatabaseHandler.Instance.GetAllData<Video>().All(v => bvid != v.BvId))
                continue;

            var videoStat = videoInfo["stat"]!;


            var videoRecord = new VideoRecord()
                              {
                                  BvId           = bvid,
                                  TimeStamp      = TimeHelper.GetCurrentTimeStamp(),
                                  Likes          = videoStat["like"]!.GetValue<int>(),
                                  Dislikes       = videoStat["dislike"]!.GetValue<int>(),
                                  Coins          = videoStat["coin"]!.GetValue<int>(),
                                  Favorites      = videoStat["favorite"]!.GetValue<int>(),
                                  Shares         = videoStat["share"]!.GetValue<int>(),
                                  BulletComments = videoStat["danmaku"]!.GetValue<int>(),
                                  Comments       = videoStat["reply"]!.GetValue<int>(),
                                  Views          = videoStat["view"]!.GetValue<int>(),
                              };

            DatabaseHandler.Instance.AddData(videoRecord);
            Log.Info($"添加了新的视频统计信息 {video.BvId} - {video.Title}");

            video.MostRecentVideoRecordId = videoRecord.VrId;
            DatabaseHandler.Instance.UpdateData(video);


            // 每次请求每个视频信息间隔1.5s
            await Task.Delay(1500);
        }

        Log.Info($"更新全部视频统计信息完成");
    }
}