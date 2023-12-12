using Microsoft.Extensions.Logging;
using ZLogger;

namespace Jargar.Playgrounds.Loggers.Benchmarks.ZLogger;
internal static class ZLogSetup
{
    internal static ILogger SetupZLog()
    {
        // Create ZLogger
        using var factory = LoggerFactory.Create(logging =>
        {
            // optional(MS.E.Logging):clear default providers.
            logging.ClearProviders();

            //  Add Console Logging and Enable Structured Logging
            logging.AddZLoggerConsole(options => options.UseJsonFormatter());
        });
        return factory.CreateLogger(nameof(ZLoggerBenchmark));
    }
}
