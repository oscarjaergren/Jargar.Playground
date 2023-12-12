using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.Extensions.Logging;
using Serilog;
using System.ComponentModel;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Jargar.Playgrounds.Loggers.Benchmarks.SeriLogger;

#pragma warning disable CA1848 // Use the LoggerMessage delegates

[MemoryDiagnoser]
[CategoriesColumn]
[BenchmarkCategory("SeriLogger")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class SeriLogBenchmark : ILoggerMessages
{
    private readonly ILogger _seriHostLogger;

    private readonly Serilog.ILogger _seriLogger;

    public SeriLogBenchmark()
    {
        Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.Console()
             .CreateLogger();
    }

    [Benchmark, Category("SeriLogger")]
    public void Logger_Source_Generator()
    {
        LogMessages.LogStartupMessage(_seriHostLogger, 1);
    }

    [Benchmark, Category("SeriLogger")]
    public void Logger_With_Logger_Define()
    {
        LogMessages._logTestDelegate(_seriHostLogger, 1, null);
    }

    [Benchmark, Category("SeriLogger")]
    public void Logger_With_Param()
    {
        Log.Information("Test {Number}", 1);
    }

    [Benchmark, Category("SeriLogger")]
    public void Log_Simple()
    {
        Log.Information("Test");
    }
}
