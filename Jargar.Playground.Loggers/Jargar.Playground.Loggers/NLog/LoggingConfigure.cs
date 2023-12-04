using System.Text.Json;
using Amazon.Runtime;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Jargar.Playgrounds.Loggers.NLog;

internal static class LoggingConfigure
{
    internal static JsonLayout GetLayout(SessionRecord? sessionRecord, string? osVersion = null)
    {
        JsonLayout? layout = new() { IncludeEventProperties = true };
        layout.Attributes.Add(new JsonAttribute("Time", "${longdate}"));
        layout.Attributes.Add(new JsonAttribute("Level", "${level:upperCase=true}"));
        layout.Attributes.Add(new JsonAttribute("Message", "${message}"));
        layout.Attributes.Add(new JsonAttribute("Exception", "${exception:format=tostring}"));

        if (osVersion != null) layout.Attributes.Add(new JsonAttribute("OS", osVersion));

        if (sessionRecord == null) return layout;

        var sessionWithoutEmail = new
        {
            sessionRecord.UniqueId,
            sessionRecord.UserId,
            sessionRecord.OrganisationId,
            sessionRecord.CreatedDateTime,
            sessionRecord.IsOnlineMode
        };

        string? session = JsonSerializer.Serialize(sessionWithoutEmail);
        layout.Attributes.Add(new JsonAttribute("SessionRecord", session));
        layout.Attributes.Add(new JsonAttribute("SessionRecordUniqueId", sessionRecord.UniqueId.ToString()));

        return layout;
    }

    internal static LoggingConfiguration GetLoggingConfiguration(string project, SessionRecord? sessionRecord = null)
    {
        LoggingConfiguration config = new();

#if DEBUG
        const string Environment = "Development";
#endif
#if STAGING
            const string Environment = "Staging";
#endif
#if RELEASE
            const string Environment = "Production";
#endif

        DebugConfig(ref config, sessionRecord);
        ConsoleConfig(ref config);
        AwsConfig(project, ref config, Environment, sessionRecord);
        SentryConfig(ref config, Environment);

        FileConfig(ref config, "Test", "NLogFile");

        LogManager.Configuration = config;

        return config;
    }

    private static void FileConfig(ref LoggingConfiguration config, string targetName, string fileName)
    {
        FileTarget fileTarget = new()
        {
            FileName = fileName
        };
        config.AddTarget(targetName, fileTarget);
        LoggingRule rule1 = new("*", LogLevel.Debug, fileTarget);
        config.LoggingRules.Add(rule1);
    }

    /// <summary> Console Target for hosting lifetime messages to improve Docker / Visual Studio startup detection </summary>
    private static void ConsoleConfig(ref LoggingConfiguration config)
    {
        ConsoleTarget debugTarget = new()
        {
            Layout =
                "${level:truncate=4:lowercase=true}: ${logger}[0]${newline} ${message}${exception:format=tostring}"
        };

        config.AddTarget("lifetimeConsole", debugTarget);
        LoggingRule rule1 = new("*", LogLevel.Debug, debugTarget);
        config.LoggingRules.Add(rule1);
    }

    private static void DebugConfig(ref LoggingConfiguration config, SessionRecord? sessionRecord)
    {
        DebuggerTarget debugTarget = new() { Layout = GetLayout(sessionRecord) };
        config.AddTarget("debug", debugTarget);
        LoggingRule rule1 = new("*", LogLevel.Debug, debugTarget);
        config.LoggingRules.Add(rule1);
    }

    private static void AwsConfig(string project, ref LoggingConfiguration config, string environment,
        SessionRecord? sessionRecord)
    {
        const string AccessKey = "BLANK";
        const string SecretKey = "BLANK";

        string machineName = Environment.MachineName;
        string osVersion = Environment.OSVersion.ToString();

        AWSTarget awsTarget = new()
        {
            LogGroup = $"{project}/{environment}",
            Region = "eu-west-2",
            LogStreamNamePrefix = machineName,
            LogStreamNameSuffix = sessionRecord?.UniqueId.ToString(),
            Credentials = new BasicAWSCredentials(AccessKey, SecretKey),
            Layout = GetLayout(sessionRecord, osVersion)
        };

        config.AddTarget("aws", awsTarget);
        config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, awsTarget));
    }

    private static void SentryConfig(ref LoggingConfiguration config, string environment)
    {
        config.AddSentry(o =>
        {
            o.Environment = environment;

            // Optionally specify a separate format for message
            o.Layout = "${message}";
            // Optionally specify a separate format for breadcrumbs
            o.BreadcrumbLayout = "${message}";

            // The NLog integration will initialize the SDK if DSN is set:
            o.Dsn = "BLANK";

            // Debug and higher are stored as breadcrumbs (default is Info)
            o.MinimumBreadcrumbLevel = LogLevel.Info;

            // Error and higher is sent as event (default is Error)
            o.MinimumEventLevel = LogLevel.Error;

            o.AttachStacktrace = true;
            o.IncludeEventDataOnBreadcrumbs = true;

            // Send the logger name as a tag
            o.AddTag("logger", "${logger}");
        });
    }
}