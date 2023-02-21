using Quartz;

namespace Test_QuartzNet6;

public static class ServiceCollectionQuartzConfiguratorExtensions
{
    public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration configuration) where T : IJob
    {
        // Use the name of the IJob as the appsettings.json key
        string jobName = typeof(T).Name;

        // Try and load the schedule from configuration
        var configKey = $"Quartz:{jobName}";
        var configurationSection = configuration.GetSection(configKey);
        var options = new GenericCronJobOptions();
        configurationSection.Bind(options);

        // Some minor validation
        if (string.IsNullOrEmpty(options.Schedule))
        {
            throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
        }

        // register the job 
        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(options.Schedule)); // use the schedule from configuration
    }
}