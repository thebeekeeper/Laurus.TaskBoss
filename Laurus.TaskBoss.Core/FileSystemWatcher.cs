using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laurus.TaskBoss.Core
{
    public class FileSystemWatcher : IFileSystemWatcher
    {
        public FileSystemWatcher(IPackageFactory packageFactory, ILog log, IScheduler scheduler)
        {
            _packageFactory = packageFactory;
            _log = log;
            _scheduler = scheduler;
        }

        void IFileSystemWatcher.WatchDirectory(string path)
        {
            var watcher = new System.IO.FileSystemWatcher(path);
            watcher.Created += watcher_Created;
            watcher.Deleted += watcher_Deleted;
            watcher.EnableRaisingEvents = true;
        }

        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            _log.Info("Detected new package: {0}", e.Name);
            var package = _packageFactory.CreateFromFile(e.FullPath);
            _scheduler.AddJob(package);
        }

        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _log.Info("Detected deleted package: {0}", e.Name);
        }

        private IPackageFactory _packageFactory;
        private ILog _log;
        private IScheduler _scheduler;
    }
}
