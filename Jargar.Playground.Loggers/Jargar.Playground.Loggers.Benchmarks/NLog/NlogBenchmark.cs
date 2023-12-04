using BenchmarkDotNet.Attributes;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using BenchmarkDotNet.Configs;

namespace Jargar.Playgrounds.Loggers.Benchmarks.NLog;

#pragma warning disable CA1848 // Use the LoggerMessage delegates

[MemoryDiagnoser]
[CategoriesColumn]
[BenchmarkCategory("Nlogger")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class NlogBenchmark : ILoggerMessages
{
    private readonly ILogger _nLogger;

    public NlogBenchmark()
    {
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
