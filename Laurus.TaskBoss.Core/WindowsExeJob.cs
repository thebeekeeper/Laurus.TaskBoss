using System.Diagnostics;
using Quartz;
using Laurus.TaskBoss.Core.Interfaces;
using System;

namespace Laurus.TaskBoss.Core
{
<<<<<<< HEAD
	public class WindowsExeJob : IJob
	{
		public WindowsExeJob()
		{
			_log = new Logger();
		}
=======
    public class WindowsExeJob : IJob
    {
        public WindowsExeJob()
        {
            // not injecting this becuase it gets created by quartz - could use the quartz facility, but i don't like 
            // that you have to use the xml config
            _log = new Logger();
        }
>>>>>>> 43d92b6885c556c8331b71e12ea027da2c7bf59e

		public void Execute(IJobExecutionContext context)
		{
			_log.Debug("Executing windows exe job");
			var exe = context.MergedJobDataMap.Get("exe_name") as string;
			string cmd = string.Empty;
			string args = string.Empty;
			if (exe.IndexOf(' ') > 0)
			{
				args = exe.Substring(exe.IndexOf(' '));
				cmd = exe.Substring(0, exe.IndexOf(' '));
			}
			else
			{
				cmd = exe;
			}
			var wdir = context.MergedJobDataMap.Get("working_dir") as string;
			var startInfo = new ProcessStartInfo(cmd);
			startInfo.Arguments = args;
<<<<<<< HEAD
			startInfo.WorkingDirectory = wdir;
			var startedProcess = System.Diagnostics.Process.Start(startInfo);
		}
=======
            startInfo.WorkingDirectory = wdir;
            try
            {
                var startedProcess = System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception e)
            {
                _log.Error("Exception caught while starting {0}: {1}", exe, e.Message);
                _log.Error("Working directory: {0}", wdir);
            }
        }
>>>>>>> 43d92b6885c556c8331b71e12ea027da2c7bf59e

		private ILog _log;
	}
}
