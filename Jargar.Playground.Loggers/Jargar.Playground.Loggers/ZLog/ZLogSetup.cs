using Microsoft.Extensions.Logging;
using ZLogger;

namespace Jargar.Playgrounds.Loggers.ZLog;
internal static class ZLogSetup
{
    internal static ILogger SetupZLog()
    {
        // Create ZLogger
        using var factory = LoggerFactory.Create(logging =>
        {
            // optional(MS.E.Logging):clear default providers.
            logging.ClearProviders();

            logging.SetMinimumLevel(LogLevel.Trace);

            logging.AddZLoggerConsole();
        });
        return factory.CreateLogger(nameof(ZLogSetup));
    }
}
