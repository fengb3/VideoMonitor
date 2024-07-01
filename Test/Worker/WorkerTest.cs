using BiliBiliApi.Apis;
using BiliBiliApi.Web_Interface;
using Lib.DataManagement;
using Microsoft.Extensions.Logging;
using Moq;
using Quartz;
using Worker.Jobs;

namespace VideoMonitor.Test.Worker;

[TestFixture]
public class WorkerTest
{
    [TestCase("BV1om421V781")]
    public async Task TestFetchVideoInfo(string bvid)
    {
        
        var logger = new Mock<ILogger<FetchVideoInfoJob>>();
        
        var dbContext = new Mock<MonitorDbContext>();
        
        var bilibiliApi = new Mock<IBilibiliApi>();
        
        bilibiliApi.Setup(x => x.WebInterfaceView(null, bvid)).ReturnsAsync(new View.Response()
        {
            Code = 0,
            Message = "OK",
            Data = new View.Data()
            {
                Stat = new View.Stat()
                {
                    Like = 100,
                    Coin = 100,
                    Favorite = 100,
                    Share = 100,
                    Danmaku = 100,
                    Reply = 100,
                }
            }
        });
        
        var job = new FetchVideoInfoJob(logger.Object, dbContext.Object, bilibiliApi.Object);
        
        var context = new Mock<IJobExecutionContext>();
        
        context.Setup(x => x.JobDetail.JobDataMap.GetString("BvId")).Returns(bvid);
        
        await job.Execute(context.Object);
    }
}