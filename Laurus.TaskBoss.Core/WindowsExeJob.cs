﻿using System.Diagnostics;
using Quartz;
using Laurus.TaskBoss.Core.Interfaces;

namespace Laurus.TaskBoss.Core
{
    public class WindowsExeJob : IJob
    {
        public WindowsExeJob()
        {
            _log = new Logger();
        }

        public void Execute(IJobExecutionContext context)
        {
            _log.Debug("Executing windows exe job");
            var exe = context.MergedJobDataMap.Get("exe_name") as string;
			var args = exe.Substring(exe.IndexOf(' '));
			var cmd = exe.Substring(0, exe.IndexOf(' '));
            var wdir = context.MergedJobDataMap.Get("working_dir") as string;
            var startInfo = new ProcessStartInfo(cmd);
			startInfo.Arguments = args;
            startInfo.WorkingDirectory = wdir;
            var startedProcess = System.Diagnostics.Process.Start(startInfo);
        }

        private ILog _log;
    }
}