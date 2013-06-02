using Castle.Windsor;
using Laurus.TaskBoss.Core;
using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Service
{
    public partial class TaskBossService : ServiceBase
    {
        public TaskBossService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _container = new WindsorContainer();
            _container.Install(new Installer());
            ILog log = _container.Resolve<ILog>();

            IScheduler scheduler = _container.Resolve<IScheduler>();
            log.Info("Starting scheduler");
            // HACK: scheduler needs to be started before the file system watcher because
            // the watcher adds jobs to the scheduler
            scheduler.Start();

            log.Info("Starting file system watcher");
            _container.Resolve<IFileSystemWatcher>().WatchDirectory(@"C:\temp\tasks");

            log.Info("Starting http server on port 8000");
            _container.Resolve<IHttpListener>().Listen(8000);
        }

        protected override void OnStop()
        {
            _container.Dispose();
        }

        private IWindsorContainer _container;
    }
}
