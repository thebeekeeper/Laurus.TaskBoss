﻿using Laurus.TaskBoss.Core.Interfaces;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Core
{
    public class Logger : ILog
    {
        public Logger()
        {
            var config = new LoggingConfiguration();
            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            consoleTarget.Layout = "${date:format=HH:MM:ss} ${logger} ${message}";
            LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);
            LogManager.Configuration = config;
            _logger = LogManager.GetLogger("Log");
        }

        void ILog.Debug(string message, params object[] items)
        {
            _logger.Debug(message, items);
        }

        void ILog.Info(string message, params object[] items)
        {
            _logger.Info(message, items);
        }

        void ILog.Error(string message, params object[] items)
        {
            _logger.Error(message, items);
        }

        void ILog.Fatal(string message, params object[] items)
        {
            _logger.Fatal(message, items);
        }

        private NLog.Logger _logger;
    }
}