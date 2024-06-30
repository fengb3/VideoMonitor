using Quartz;
using Lib.DataManagement;
using BiliBiliApi.Apis;
using Microsoft.EntityFrameworkCore;

namespace Worker.Jobs;

public class FetchUploaderInfoJob(
    ILogger<FetchUploaderInfoJob> logger,
    MonitorDbContext dbContext,
    IBilibiliApi bilibiliApi) : IJob
{
    /// <summary>
    /// 从B站 获取上传者信息
    /// 1.	上传者的基本信息（名称、头像、粉丝、存档、点赞） 
    ///     保存为用户和用户记录
    /// 2.	上传者的视频列表 
    ///     保存为视频
    /// </summary>
    /// <param name="jobExecutionContext">工作执行上下文</param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext jobExecutionContext)
    {
        var uploaderId = jobExecutionContext.JobDetail.JobDataMap.GetLong("uploaderId");

        var uploaderInfo = await bilibiliApi.WebInterfaceCard(uploaderId, "true");

        logger.LogInformation($"Fetched uploader info: {uploaderInfo.Data}");

        var user = await dbContext.Users.FindAsync(uploaderId);

        user ??= new Model.User()
            {
                Uid = uploaderId,
                Name = uploaderInfo.Data.Card!.Name,
                FaceUrl = uploaderInfo.Data.Card.Face.ToString(),
            };

        var userRecord = new Model.UserRecord()
        {
            User = user,
            Uid = uploaderId,
            TimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
            FollowerNum = uploaderInfo.Data.Follower,
            ArchiveNum = uploaderInfo.Data.ArchiveCount,
            LikeNum = uploaderInfo.Data.LikeNum,
        };

        user.MostRecentUserRecord = userRecord;

        await dbContext.SaveChangesAsync();

        var uploadersVideoInfo = await bilibiliApi.SpaceArcSearch(uploaderId);

        var videos = new List<Model.Video>();

        foreach (var video in uploadersVideoInfo.Data.List.Vlist)
        {
            var videoModel = new Model.Video()
            {
                BvId = video.Bvid,
                Title = video.Title,
                Description = video.Description,
                CoverUrl = video.Pic,
                UploadTimeStamp = video.Created,
            };

            videos.Add(videoModel);
        }


        var existingVideos = await dbContext.Videos.Where(v => videos.Select(v => v.BvId).Contains(v.BvId)).ToListAsync();

        foreach (var video in videos)
        {
            var existingVideo = existingVideos.FirstOrDefault(v => v.BvId == video.BvId);

            if (existingVideo == null)
            {
                dbContext.Videos.Add(video);
            }
        }

        await dbContext.SaveChangesAsync();

        // return Task.CompletedTask;
    }
}
