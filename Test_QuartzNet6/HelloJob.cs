using Microsoft.Extensions.Options;
using Quartz;

namespace Test_QuartzNet6
{
    [DisallowConcurrentExecution]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly HelloWorldJobOptions _helloWorldJobOptions;
        public HelloWorldJob(ILogger<HelloWorldJob> logger, IOptions<HelloWorldJobOptions> helloWorldJobOptions)
        {
            _logger = logger;
            _helloWorldJobOptions = helloWorldJobOptions.Value;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}
