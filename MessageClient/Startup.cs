using System;
using MessageClient.Infrastructure;
using MessageClient.Infrastructure.Db;
using MessageClient.Infrastructure.Jobs;
using MessageClient.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MessageClient
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new MessageServerConfiguration(_configuration);

            services.AddSingleton(configuration);

            services.AddMvc()
                .AddMvcOptions(x => x.EnableEndpointRouting = false);
            services.AddDbContext<MessageDbContext>(x =>
                x.UseSqlServer(configuration.ConnectionString));

            services.AddHttpClient("httpClient", x => x.Timeout = TimeSpan.FromSeconds(configuration.ConnectionTimeout));
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageSender, MessageSender>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<MessageSenderJob>();
            services.AddSingleton(new JobSchedule(
                typeof(MessageSenderJob),
                configuration.MessageSenderJobCronTime));

            services.AddHostedService<QuartzHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
