using Microsoft.Extensions.Logging;

namespace Jargar.Playgrounds.Loggers.Benchmarks;
internal static partial class LogMessages
{
    internal static readonly Action<ILogger, int, Exception?> _logTestDelegate =
       LoggerMessage.Define<int>(
           logLevel: LogLevel.Information,
           eventId: 0,
           formatString: "Test {Number}");

    [LoggerMessage(Level = LogLevel.Information, Message = "Test {Number}.")]
    internal static partial void LogStartupMessage(ILogger logger, int number);
}
