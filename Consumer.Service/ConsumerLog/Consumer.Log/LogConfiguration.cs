using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Consumer.Log
{
    public static class LogConfiguration
    {
        private const string LOG_LIBRARY_NAME = "Consumer.Log";
        private const string CONSUMER_LOG_DIRECTORY = "ConsumerLog";
        private const string CONSUMER_LOG_FILE_NAME = "ConsumerLog-.txt";
        private const string CONSUMER_PROJECT_NAME = "Consumer.Service";

        public static ILogger GenerateConsumerLog()
        {
            var logFilePathForConsumerApplicationApi = GetFilePath(logApiDirName: CONSUMER_LOG_DIRECTORY, logFileName: CONSUMER_LOG_FILE_NAME);
            return CreateLog(logPath: logFilePathForConsumerApplicationApi, projectName: CONSUMER_PROJECT_NAME);
        }

        private static string GetFilePath(string logApiDirName, string logFileName)
        {
            var logDirectory = GetProjectPath();
            var logPath = Path.Combine(logDirectory, logFileName);
            return logPath;
        }

        private static Logger CreateLog(string logPath, object projectName)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty(name: "ApplicationName", value: projectName)
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File
                    (
                        path: logPath,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{ApplicationName}] {Message:lj}{NewLine}{Exception} \n",
                        retainedFileCountLimit: 7
                    ).CreateLogger();
        }

        private static string GetProjectPath()
        {
            var currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);

            // Keep going up until we find the folder "Consumer.Service"
            while (currentDirectory != null && currentDirectory.Name != CONSUMER_PROJECT_NAME)
            {
                currentDirectory = currentDirectory.Parent;
            }

            /* Get one level up folder of current directory */
            currentDirectory = currentDirectory?.Parent;

            if (currentDirectory is null)
            {
                throw new Exception("Could not find the project root folder!");
            }

            return Path.Combine(currentDirectory.FullName, CONSUMER_LOG_DIRECTORY, LOG_LIBRARY_NAME, CONSUMER_LOG_DIRECTORY);
        }
    }
}