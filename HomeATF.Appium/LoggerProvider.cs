using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeATF.Appium.Interfaces;
using NLog;
using NLog.Config;
using NLog.Layouts;

namespace HomeATF.Appium
{
    public class LoggerProvider
    {
        public static ILogger GetLogger(ISettings settings)
        {
            var configuration = new LoggingConfiguration();
            
            var fileTarget = new NLog.Targets.FileTarget("file")
            {
                FileName = settings.LogFilePath,
                Layout = @"${longdate} ${uppercase: ${level}} ${message}",
            };

            var debugTarget = new NLog.Targets.DebuggerTarget("debug")
            { 
                Layout = @"${uppercase: ${level}} ${message}",
            };

            configuration.AddTarget(fileTarget);
            configuration.AddTarget(debugTarget);

            configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, debugTarget);
            LogManager.Configuration = configuration;

            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("----------------------New Log Started--------------------------");
            logger.Info($"Started logging to {settings.LogFilePath} at {DateTime.Now}");

            return logger;
        }
        
    }
}
