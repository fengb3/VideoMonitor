// using Worker;
//
// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker.Worker>();
//
// var host = builder.Build();
// host.Run();

using Quartz;

public class JobScheduler
{
    public static async Task ChangeJobExecutionInterval(IScheduler scheduler, JobKey jobKey, TimeSpan newInterval)
    {
        // 获取所有与该作业关联的触发器
        var triggers = await scheduler.GetTriggersOfJob(jobKey);
        foreach (var trigger in triggers)
        {
            // 创建一个新的触发器，使用新的执行间隔
            var newTrigger = TriggerBuilder.Create()
                .WithIdentity(trigger.Key)
                .ForJob(jobKey)
                .WithSimpleSchedule(x => x
                    .WithInterval(newInterval)
                    .RepeatForever())
                .Build();

            // 使用新的触发器替换旧的触发器
            await scheduler.RescheduleJob(trigger.Key, newTrigger);
        }
    }
}