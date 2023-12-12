using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Serilog;
using Jargar.Playgrounds.Loggers.Benchmarks.SeriLogger;
using ZLogger.Providers;
using Jargar.Playgrounds.Loggers.ZLog;
using ZLogger;

//#NLog
//LogManager.Setup().LoadConfiguration(builder => builder.ForLogger().FilterMinLevel(NLog.LogLevel.Info).WriteToConsole());
//var logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
//logger.LogInformation("Program has started.");


//#ZLogger
using var factory = LoggerFactory.Create(logging =>
{
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddZLoggerConsole();
});
var logger = factory.CreateLogger("Program");
logger.ZLogInformation($"Test");

//#SeriLog
//Log.Logger = new LoggerConfiguration()
//          .MinimumLevel.Debug()
//          .WriteTo.Console()
//          .CreateLogger();

//Log.Information("Hello, world!");