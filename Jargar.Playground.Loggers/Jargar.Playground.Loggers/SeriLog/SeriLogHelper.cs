using Serilog;

namespace Jargar.Playgrounds.Loggers.Benchmarks.SeriLogger;

/// <summary>
///     https://github.com/serilog/serilog
/// </summary>
public sealed class SeriLogHelper
{
    public static ILogger InitializeSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Async(writeTo => writeTo.Console())
            .CreateLogger();

        return Log.Logger;
    }
}