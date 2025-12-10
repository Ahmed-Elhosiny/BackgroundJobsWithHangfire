using BackgroundJobsWithHangfire.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobsWithHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController(IJobTestService jobTestService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager) : ControllerBase
    {
        private readonly IJobTestService _jobTestService = jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromSeconds(60));
            return Ok();
        }
        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobTestService.RecurringJob(), Cron.Minutely);
            return Ok();
        }
        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            string? parentJobId = _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobTestService.ContinuationJob());

            return Ok();
        }

    }
}
