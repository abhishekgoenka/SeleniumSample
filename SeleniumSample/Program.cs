using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeleniumSample.Pages;
using SeleniumSample.Repository;
using System.Threading.Tasks;

namespace SeleniumSample
{
    class Program
    {

        static async Task Main(string[] args)
        {

            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsoleHostedService>()
                .AddSingleton<LoginPage>()
                .AddSingleton<Dashboard>()
                .AddSingleton<Appointment>()
                .AddDbContext<VaccineDbContext>(context =>
                {
                    context.UseSqlite(hostContext.Configuration.GetConnectionString("sqliteConnectionString"));
                });
            });
    }
}
