using Quartz;

namespace Worker.Jobs;

class HelloJob(ILogger<HelloJob> logger) : IJob
{
    public static int ExecuteCount { get; set; }

    public Task Execute(IJobExecutionContext context)
    {
        // var jobsSays = context.JobDetail.JobDataMap.GetString("jobSays");
        var name = context.JobDetail.JobDataMap.GetString("name");

        using var scope = logger.BeginScope("gg");
        {
            logger.LogInformation(new EventId(233, "gg"),
                $"Execute Job {name} at {DateTimeOffset.Now} -- {++ExecuteCount}");
        }

        return Task.CompletedTask;
    }
}