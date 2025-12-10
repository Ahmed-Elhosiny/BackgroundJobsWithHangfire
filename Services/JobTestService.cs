namespace BackgroundJobsWithHangfire.Services
{
    public class JobTestService(ILogger<JobTestService> logger) : IJobTestService
    {
        private readonly ILogger _logger = logger;

        public void ContinuationJob()
        {
            _logger.LogInformation("Hello from a Continuation job!");
        }

        public void DelayedJob()
        {
            _logger.LogInformation("Hello from a Delayed job!");
        }

        public void FireAndForgetJob()
        {
            _logger.LogInformation("Hello from a Fire and Forget job!");
        }

        public void RecurringJob()
        {
            _logger.LogInformation("Hello from a Scheduled job!");
        }
    }
}
