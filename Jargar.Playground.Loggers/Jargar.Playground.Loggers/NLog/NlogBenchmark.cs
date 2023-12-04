namespace Jargar.Playgrounds.Loggers.NLog;

internal class NlogBenchmark
{
    //private static Logger StaticNLogger;

    //private readonly ILogger _nLogger;

    //public NlogBenchmark()
    //{
    //    ILoggerFactory nLoggerFactory = LoggerFactory.Create(builder => builder.AddNLog());
    //    _nLogger = new Logger<NlogBenchmark>(nLoggerFactory);
    //    LoggingConfiguration config = new();
    //    DebugConfig(ref config);
    //    LogManager.Configuration = config;
    //    StaticNLogger = LogManager.GetCurrentClassLogger();
    //}

    //static NlogBenchmark()
    //{
    //    StaticNLogger = LogManager.GetCurrentClassLogger();
    //}

    //[Benchmark]
    //public void HostNlogger()
    //{
    //    _nLogger.LogInformation("Test");
    //}

    ////[Benchmark]
    ////public static void StaticNlogger()
    ////{
    ////    StaticNLogger.Info("Test {Number}", 1);
    ////}

    //[Benchmark]
    //public void HostNlogger_With_Param()
    //{
    //    _nLogger.LogInformation("Test {Number}", 1);
    //}

    //private static void DebugConfig(ref LoggingConfiguration config)
    //{
    //    DebuggerTarget debugTarget = new();
    //    config.AddTarget("debug", debugTarget);
    //    LoggingRule rule1 = new("*", LogLevel.Debug, debugTarget);
    //    config.LoggingRules.Add(rule1);
    //}
}
