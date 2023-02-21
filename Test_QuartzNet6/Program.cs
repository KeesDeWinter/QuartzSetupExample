using Quartz;
using System;

namespace Test_QuartzNet6
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // Add the required Quartz.NET services
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJobAndTrigger<HelloWorldJob>(builder.Configuration);
                // more jobs here
            });

            // optional, needed in case a job has additional options
            builder.Services.Configure<HelloWorldJobOptions>(builder.Configuration.GetSection(nameof(HelloWorldJobOptions)));

            // Add the Quartz.NET hosted service
            builder.Services.AddQuartzHostedService(
                // When shutdown is requested, this setting ensures that Quartz.NET waits for the jobs to end gracefully before exiting.
                q => q.WaitForJobsToComplete = true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}