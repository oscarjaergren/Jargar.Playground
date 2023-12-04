using BenchmarkDotNet.Running;
using Logging;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BestPracticesConsoleApp;

internal sealed class ConsoleHostedService(
    ILogger logger,
    IHostApplicationLifetime appLifetime) : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime = appLifetime;
    private readonly ILogger _logger = logger;
    private int? _exitCode;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.Debug("Starting with arguments: {V}", string.Join(" ", Environment.GetCommandLineArgs()));

        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(() =>
            {
                try
                {
                    _logger.Debug("Hello World!");

                    // Simulate real work is being done
                    var summary = BenchmarkRunner.Run<LoggerBenchmark>();
                    _exitCode = 0;
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex, "Unhandled exception!");
                    _exitCode = 1;
                }
                finally
                {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            }, cancellationToken);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.Debug("Exiting with return code: {ExitCode}", _exitCode);

        // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
        Environment.ExitCode = _exitCode ?? -1;
        return Task.CompletedTask;
    }
}