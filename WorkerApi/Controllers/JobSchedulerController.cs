using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl.Matchers;
using System.Text.Json;
using Worker.Jobs;

namespace Worker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSchedulerController : ControllerBase
    {

        /// <summary>
        /// JSON序列化配置
        /// </summary>
        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// 定时器工厂
        /// </summary>
        private readonly ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// 定时器
        /// </summary>
        private readonly IScheduler _scheduler;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="schedulerFactory"></param>
        public JobSchedulerController(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 创建作业并调度
        /// </summary>
        /// <param name="request">创建作业请求</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public async Task<IActionResult> CreateJobCorn([FromBody]CreateJobRequest request)
        {
            if(request.JobName == null || request.CronExpression == null)
            {
                return BadRequest("JobName and CronExpression are required");
            }

            var jobKey = new JobKey(request.JobName);

            // check if job exists
            if (await _scheduler.CheckExists(jobKey))
            {
                return Conflict($"a job with name of {request.JobName} already exist");
            }

            var jobDetailBuilder = JobBuilder.Create<HelloJob>()
                .WithIdentity(jobKey);

            var parameters = JsonSerializer.Deserialize<Dictionary<string, string>>(request.JobParameters, jsonOptions);

            parameters ??= []; // 如果参数为空则初始化为空数组

            foreach (var parameter in parameters)
            {
                jobDetailBuilder.UsingJobData(parameter.Key, parameter.Value);
            }

            var jobDetail = jobDetailBuilder.Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobKey.Name}-trigger")
                .ForJob(jobKey)
                .WithCronSchedule(request.CronExpression)
                .Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);

            return Created();
        }

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <returns>操作结果</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteJob([FromBody] string jobName)
        {
            var jobKey = new JobKey(jobName);

            // check if job exists
            if (!await _scheduler.CheckExists(jobKey))
            {
                return NotFound();
            }

            await _scheduler.DeleteJob(jobKey);

            return Ok();
        }

        /// <summary>
        /// 更改作业的Cron表达式
        /// </summary>
        /// <param name="request">更改作业Cron请求</param>
        /// <returns>操作结果</returns>
        [HttpPut("corn")]
        public async Task<IActionResult> ChangeJobCron([FromBody] ChangeJobCornRequest request)
        {
            var jobKey = new JobKey(request.JobName);
            var triggers = await _scheduler.GetTriggersOfJob(jobKey);
            foreach (var trigger in triggers)
            {
                var newTrigger = TriggerBuilder.Create()
                    .WithIdentity(trigger.Key)
                    .ForJob(jobKey)
                    .WithCronSchedule(request.CronExpression) // 使用新的Cron表达式
                .Build();

                await _scheduler.RescheduleJob(trigger.Key, newTrigger);
            }

            return Ok();
        }

        /// <summary>
        /// 更改作业的参数
        /// </summary>
        /// <param name="request">更改作业参数请求</param>
        /// <returns></returns>
        [HttpPut("parameters")]
        public async Task<IActionResult> ChangeJobParameters([FromBody] ChangeJobParametersRequest request)
        {
            if(request.JobName == null)
            {
                return BadRequest("JobName is required");
            }

            var jobKey = new JobKey(request.JobName);
            var jobDetail = await _scheduler.GetJobDetail(jobKey);

            if (jobDetail == null)
            {
                return NotFound();
            }

            await _scheduler.DeleteJob(jobKey);

            var parameters = JsonSerializer.Deserialize<Dictionary<string, string>>(request.JobParameters, jsonOptions);

            parameters ??= []; // 如果参数为空则初始化为空数组

            foreach (var parameter in parameters)
            {
                jobDetail.JobDataMap.Put(parameter.Key, parameter.Value);
            }

            await _scheduler.AddJob(jobDetail, true);

            return Ok();
        }


            /// <summary>
            /// 获取所有作业
            /// </summary>
            /// <returns>作业列表</returns>
            [HttpGet("all")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            var jobs = new List<IJobDetail>();

            foreach (var jobKey in jobKeys)
            {
                var job = await _scheduler.GetJobDetail(jobKey);
                if (job != null) // Check if job is not null before adding
                {
                    jobs.Add(job);
                }
            }

            return Ok(jobs);
        }

        public class CreateJobRequest
        {
            public string? JobName { get; set; }// 默认Job名称
            public string? CronExpression { get; set; } // 每5秒执行一次
            public JsonElement JobParameters { get; set; } // Job参数
        }

        public class ChangeJobCornRequest
        {
            public string? JobName { get; set; }
            public string? CronExpression { get; set; }
        }

        public class ChangeJobParametersRequest
        {
            public string? JobName { get; set; }
            public JsonElement JobParameters { get; set; }
        }

    }
}
