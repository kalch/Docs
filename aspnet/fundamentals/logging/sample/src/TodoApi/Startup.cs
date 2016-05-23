using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApi.Core.Interfaces;
using TodoApi.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace TodoApi
{
    public class Startup
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add our repository type
            services.AddScoped<ITodoRepository, TodoRepository>();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory
                .WithFilter(new FilterLoggerSettings
                {
                    { "Microsoft", LogLevel.Warning },
                    { "System", LogLevel.Warning },
                    { "ToDoApi", LogLevel.Debug }
                });

            loggerFactory.AddConsole();

#if NET461
            var sourceSwitch = new SourceSwitch("Logging Sample");
            sourceSwitch.Level = SourceLevels.Error;
            loggerFactory.AddTraceSource(sourceSwitch, 
                new EventLogTraceListener("Application"));
#endif
            app.UseStaticFiles();

            app.UseMvc();

            // Create a catch-all response
            app.Run(async (context) =>
            {
                if (context.Request.Path.Value.Contains("boom"))
                {
                    throw new Exception("boom!");
                }
                var logger = loggerFactory.CreateLogger("Catchall Endpoint");
                logger.LogInformation("No endpoint found for request {path}", context.Request.Path);
                await context.Response.WriteAsync("No endpoint found - try /api/todo.");
            });
        }
    }
}