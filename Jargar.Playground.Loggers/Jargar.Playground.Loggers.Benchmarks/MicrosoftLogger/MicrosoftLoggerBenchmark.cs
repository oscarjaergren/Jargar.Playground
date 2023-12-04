using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Jargar.Playgrounds.Loggers.Benchmarks.MicrosoftLogger;

#pragma warning disable CA1848 // Use the LoggerMessage delegates

[MemoryDiagnoser]
[CategoriesColumn]
[BenchmarkCategory("MicrosoftLogger")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class MicrosoftLoggerBenchmark : ILoggerMessages
{
    private readonly ILogger _microsoftLogger;

    public MicrosoftLoggerBenchmark()
    {
        // Create Microsoft logger
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        _microsoftLogger = factory.CreateLogger(nameof(MicrosoftLoggerBenchmark));
    }

    [Benchmark, Category("MicrosoftLogger")]
    public void Log_Simple()
    {
        _microsoftLogger.LogInformation("Test");
    }

    [Benchmark, Category("MicrosoftLogger")]

    public void Logger_With_Param()
    {
        _microsoftLogger.LogInformation("Test {Number}", 1);
    }

    [Benchmark, Category("MicrosoftLogger")]
    public void Logger_With_Logger_Define()
    {
        LogMessages._logTestDelegate(_microsoftLogger, 1, null);
    }

    [Benchmark, Category("MicrosoftLogger")]
    public void Logger_Source_Generator()
    {
        LogMessages.LogStartupMessage(_microsoftLogger, 1);
    }
}