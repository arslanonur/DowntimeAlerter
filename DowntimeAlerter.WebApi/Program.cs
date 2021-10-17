using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DowntimeAlerter.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            try
            {
                //Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //Log.Fatal(ex, "Application start-up failed");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config => { config.AddConfiguration(configSettings); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}