using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MT.E_Sourcing.Order.Extentions;

namespace MT.E_Sourcing.Order
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                .MigrateDatabase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
