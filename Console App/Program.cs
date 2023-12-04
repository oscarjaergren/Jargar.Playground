using System.Reflection;
using Logging.SeriLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BestPracticesConsoleApp;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var seriLogLogger = SeriLogHelper.InitializeSerilog();
        string? contentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        //SeriLogHelper.InitializeSerilog();
        //Log.Information("Hello, world!");

        await Host.CreateDefaultBuilder(args)
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddHostedService<ConsoleHostedService>();
            })
            .UseSerilog(seriLogLogger)
            .RunConsoleAsync();
    }

    public static IHost BuildHost(string[] args)
    {
        return new HostBuilder()
            .UseSerilog() // <- Add this line
            .Build();
    }
}