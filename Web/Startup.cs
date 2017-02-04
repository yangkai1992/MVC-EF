using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire.SqlServer;
using Hangfire;
using Hangfire.SqlServer.RabbitMQ;
using Common;

[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //MSMQ
            //GlobalConfiguration.Configuration
            //.UseSqlServerStorage("DefaultConnection")
            //.UseMsmqQueues(@".\Private$\{0}", "default", "test");

            //rabbitMQ
            var sqlStorage = new SqlServerStorage(ConfigHelper.GetConnectionString("DefaultConnection")).UseRabbitMq(conf =>
            {
                conf.HostName = "localhost";
                conf.Port = 5672;
                conf.Username = "yangkai";
                conf.Password = "123456";
            }, "default", "test");

            GlobalConfiguration.Configuration.UseStorage(sqlStorage);
            var options = new BackgroundJobServerOptions
            {
                Queues = new string[] { "default", "test" },
                ServerName = "SQL队列服务"
            };
            app.UseHangfireServer(options);
            app.UseHangfireDashboard();
        }
    }
}
