using System.Reflection;

namespace Jargar.Playgrounds.Loggers.NLog;

internal static class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, long milliseconds, string message)
    {
        StaticLogger.CurrentLogger.Info("Method {MethodName} took {Time} + {message}",
            methodBase.Name,
            milliseconds,
            message);
    }
}