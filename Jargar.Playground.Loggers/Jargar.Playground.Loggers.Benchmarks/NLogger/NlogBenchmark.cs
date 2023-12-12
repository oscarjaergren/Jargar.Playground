using NLog.Extensions.Logging;
using NLog;
using Microsoft.Extensions.Logging;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.ComponentModel;

namespace Jargar.Playgrounds.Loggers.Benchmarks.NLogger;

#pragma warning disable CA1848 // Use the LoggerMessage delegates

[MemoryDiagnoser]
[CategoriesColumn]
[BenchmarkCategory("Nlogger")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class NlogBenchmark : ILoggerMessages
{
    private readonly Microsoft.Extensions.Logging.ILogger _nLogger;

    public NlogBenchmark()
    {
        LogManager.Setup().LoadConfiguration(builder => builder.ForLogger().FilterMinLevel(NLog.LogLevel.Info).WriteToConsole());

        _nLogger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
    }

    [Benchmark, Category("Nlogger")]
    public void Log_Simple()
    {
        _nLogger.LogInformation("Test");
    }

    [Benchmark, Category("Nlogger")]
    public void Logger_With_Param()
    {
        _nLogger.LogInformation("Test {1}", 1);
    }

    [Benchmark, Category("Nlogger")]
    public void Logger_With_Logger_Define()
    {
        LogMessages._logTestDelegate(_nLogger, 1, null);
    }

    [Benchmark, Category("Nlogger")]
    public void Logger_Source_Generator()
    {
        LogMessages.LogStartupMessage(_nLogger, 1);
    }
}
