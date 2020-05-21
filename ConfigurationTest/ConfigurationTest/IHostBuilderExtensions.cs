using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using NetEscapades.Extensions.Logging.RollingFile;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConfigurationTest
{
    public static class IHostBuilderExtensions
    {
        public static IHostBuilder ConfigureServices(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });

            return builder;
        }

        public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
        {
            builder.ConfigureLogging(loggingBuilder =>
            {
                if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == Environments.Development)
                {
                    loggingBuilder.AddFile(options =>
                    {
                        SetFileLoggerOptions(options);
                    });

                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                }
                else
                {
                    loggingBuilder.AddFile(options =>
                    {
                        SetFileLoggerOptions(options);
                    });

                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                }
            });

            return builder;
        }

        public static IHostBuilder SetCurrentDirectory(this IHostBuilder builder, string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            if (isService)
            {
                var processModule = Process.GetCurrentProcess().MainModule;
                if (processModule != null)
                {
                    var pathToExe = processModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    Directory.SetCurrentDirectory(pathToContentRoot);
                }
            }

            return builder;
        }

        public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder builder)
        {            
            builder.ConfigureAppConfiguration(config =>
            {
                config.Add(new JsonFileConfigurationSource());
            });

            return builder;
        }

        private static void SetFileLoggerOptions(FileLoggerOptions options)
        {
            options.FileName = "log-";
            options.LogDirectory = "logs";
            options.FileSizeLimit = 30 * 1024 * 1024;
            options.Extension = "log";
            options.Periodicity = PeriodicityOptions.Daily;
        }

        private static void SetEventLoggerOptions(EventLogSettings options)
        {
            //options.LogName = "Ble-Service";
        }
    }
}
