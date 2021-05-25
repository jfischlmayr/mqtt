using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_DataLogger_RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new DataLoggerContextFactory();
            var context = factory.CreateDbContext();

            CreateHostBuilder(args).Build().Run();

            var logic = new Logic(context);
            logic.Subscribe();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
