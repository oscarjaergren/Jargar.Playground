using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using ZLogger;

namespace Jargar.Playgrounds.Loggers.Benchmarks.ZLogger;

/// <summary>
///     https://github.com/Cysharp/ZLogger
/// </summary>
[MemoryDiagnoser]
[CategoriesColumn]
[BenchmarkCategory("ZLogger")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ZLoggerBenchmark : ILoggerMessages
{
    private readonly ILogger _zLogger;

    public ZLoggerBenchmark()
    {
        using var factory = LoggerFactory.Create(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddZLoggerConsole();
        });

        _zLogger = factory.CreateLogger("Program");

    }

    [Benchmark, Category("ZLogger")]
    public void Logger_Source_Generator()
    {
        LogMessages.LogStartupMessage(_zLogger, 1);
    }

    [Benchmark, Category("ZLogger")]
    public void Logger_With_Logger_Define()
    {
        LogMessages._logTestDelegate(_zLogger, 1, null);
    }

    [Benchmark, Category("ZLogger")]
    public void Logger_With_Param()
    {
        _zLogger.ZLogInformation(1, $"Test {1}");
    }

    [Benchmark, Category("ZLogger")]
    public void Log_Simple()
    {
        _zLogger.ZLogInformation($"Test");
    }
}