using BiliBiliApi.Apis;
using Lib.DataManagement;
using Quartz;

namespace Worker.Jobs;


public class FetchVideoInfoJob ( ILogger<FetchVideoInfoJob> logger,
                                 MonitorDbContext              dbContext,
                                 IBilibiliApi                  bilibiliApi): IJob
{
    /// <summary>
    /// 从B站 获取视频信息
    /// 记录为video Record
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task Execute(IJobExecutionContext context)
    {
        var bvId = context.JobDetail.JobDataMap.GetString("BvId");

        if (bvId == null)
        {
            throw new Exception("FetchVideoInfoJob Error: Cannot found BvId in JobDataMap");
        }
        
        var videoInfo = await bilibiliApi.WebInterfaceView(bvid: bvId);

        if (videoInfo.Data == null)
        {
            throw new Exception("FetchVideoInfoJob Error: Incorrectlly Get Video Info from Bilibili api");
        }
        
        logger.LogInformation($"Fetched video info: {videoInfo.Data}");
        
        var video = await dbContext.Videos.FindAsync(bvId);

        if (video == null)
        {
            throw new Exception($"FetchVideoInfoJob Error: Cannot found Video with {bvId} in database");
        }
        
        var videoRecord = new Model.VideoRecord()
        {
            Video = video,
            BvId = bvId,
            TimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
            Likes = videoInfo.Data.Stat.Like,
            Coins = videoInfo.Data.Stat.Coin,
            Favorites = videoInfo.Data.Stat.Favorite,
            Shares = videoInfo.Data.Stat.Share,
            Danmaku = videoInfo.Data.Stat.Danmaku,
            Comments = videoInfo.Data.Stat.Reply,
        };
        
        video.MostRecentVideoRecord = videoRecord;
        
        dbContext.Videos.Update(video);
        dbContext.VideoRecords.Add(videoRecord);
        
        await dbContext.SaveChangesAsync();
        
        throw new NotImplementedException();
    }
}