using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.AspNetCore;

namespace CarPark.User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .WriteTo.MSSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarParkLog;Persist Security Info=True;", "Logs")
                .MinimumLevel.Information()
                .WriteTo.Seq("http://localhost:5341/")
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Kod yazan", "Mahmut Yýldýz")
                .Enrich.WithThreadName().Enrich.WithThreadName()
                .Enrich.WithMachineName()
                .Enrich.WithAssemblyName()
              .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
