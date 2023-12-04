using NLog;

namespace Jargar.Playgrounds.Loggers.NLog;

/// <summary>
///     https://github.com/NLog/NLog
/// </summary>
public static class StaticLogger
{
    private static Logger? _currentLogger;

    public static Logger CurrentLogger
    {
        get => _currentLogger ?? throw new Exception("The logging needs to be configured before use");
        set => _currentLogger = value;
    }

    public static void Configure(string projectName, SessionRecord? sessionRecord = null)
    {
        LogManager.Configuration = LoggingConfigure.GetLoggingConfiguration(projectName, sessionRecord);
        CurrentLogger = LogManager.GetCurrentClassLogger();
    }
}