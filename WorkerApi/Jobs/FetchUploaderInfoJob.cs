using Quartz;
using Lib.DataManagement;
using BiliBiliApi.Apis;

namespace Worker.Jobs;

public class FetchUploaderInfoJob(
    ILogger<FetchUploaderInfoJob> logger, 
    MonitorDbContext dbContext,
    IBilibiliApi bilibiliApi) : IJob
{
    public async Task Execute(IJobExecutionContext jobExecutionContext)
    {
        var uploaderId = jobExecutionContext.JobDetail.JobDataMap.GetLong("uploaderId");

        var uploaderInfo = await bilibiliApi.WebInterfaceCard(uploaderId, "true");

        logger.LogInformation($"Fetched uploader info: {uploaderInfo.Data}");

        // throw new NotImplementedException();
    }
}
