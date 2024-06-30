// using Worker;
//
// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker.Worker>();
//
// var host = builder.Build();
// host.Run();

using Microsoft.Extensions.Hosting;
using Quartz;

var builder = Host.CreateDefaultBuilder()
	.ConfigureServices((cxt, services) =>
	{
		services.AddQuartz();
		services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });
	})
	.ConfigureLogging(logging => { logging.AddSimpleConsole(opt => { opt.IncludeScopes = true; }); });

var app = builder.Build();

var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
var scheduler        = await schedulerFactory.GetScheduler();

var job = JobBuilder.Create<HelloJob>()
	.WithIdentity("job1")
	.UsingJobData("jobSays", "Hello World!")
	.UsingJobData("name",    "GG")
	.Build();

var trigger = TriggerBuilder.Create()
	.WithIdentity("myTrigger", "group1")
	.StartNow()
	.WithSimpleSchedule(x => x
		.WithIntervalInSeconds(1)
		.WithRepeatCount(1))
	.Build();

await scheduler.ScheduleJob(job, trigger);

await app.RunAsync();


class HelloJob(ILogger<HelloJob> logger) : IJob
{
	public static int ExecuteCount { get; set; }

	public Task Execute(IJobExecutionContext context)
	{
		var jobsSays = context.JobDetail.JobDataMap.GetString("jobSays");
		var name = context.JobDetail.JobDataMap.GetString("name");

		using var scope = logger.BeginScope("gg");
		{
			logger.LogInformation(new EventId(233, "gg"),
				$"Job says \"{jobsSays}\" to {name} at {DateTimeOffset.Now} -- {++ExecuteCount}");
		}

		return Task.CompletedTask;
	}
}
