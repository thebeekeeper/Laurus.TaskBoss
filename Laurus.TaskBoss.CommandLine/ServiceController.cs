using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laurus.TaskBoss.Core.Interfaces;
using Topshelf;

namespace Laurus.TaskBoss.CommandLine
{
	public class ServiceController : ServiceControl
	{
		public ServiceController(ILog log, IScheduler scheduler, IFileSystemWatcher watcher, IHttpListener listener)
		{
			_log = log;
			_scheduler = scheduler;
			_watcher = watcher;
			_httpListener = listener;
		}

		public bool Start(HostControl hostControl)
		{
			_log.Info("Starting scheduler");
			// HACK: scheduler needs to be started before the file system watcher because
			// the watcher adds jobs to the scheduler
			_scheduler.Start();

			_log.Info("Starting file system watcher");
			_watcher.WatchDirectory(@"C:\temp\tasks");

			_log.Info("Starting http server on port 8000");
			_httpListener.Listen(8000);

			return true;
		}

		public bool Stop(HostControl hostControl)
		{
			return true;
		}

		private readonly ILog _log;
		private readonly IScheduler _scheduler;
		private readonly IFileSystemWatcher _watcher;
		private readonly IHttpListener _httpListener;
	}
}
