using Castle.Windsor;
using Laurus.TaskBoss.Core;
using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new Installer());

            ILog log = container.Resolve<ILog>();
            log.Info("Starting file system watcher");
            container.Resolve<IFileSystemWatcher>().WatchDirectory(@"C:\temp\tasks");

            IScheduler scheduler = container.Resolve<IScheduler>();
            log.Info("Starting scheduler");
            scheduler.Start();
        }
    }
}
