using System.Linq;
using System.IO;
using Laurus.TaskBoss.Core.Interfaces;

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
            var existingPackages = Directory.GetFiles(path, "*.zip").Concat(Directory.GetFiles(path, "*.json"));
            foreach (var p in existingPackages)
            {
                FileInfo fi = new FileInfo(p);
                AddPackage(fi.FullName, fi.Name);
            }
            var watcher = new System.IO.FileSystemWatcher(path);
            watcher.Created += WatcherCreated;
            watcher.Deleted += WatcherDeleted;
            watcher.EnableRaisingEvents = true;
        }

        private void WatcherCreated(object sender, FileSystemEventArgs e)
        {
            AddPackage(e.FullPath, e.Name);
        }

        private void WatcherDeleted(object sender, FileSystemEventArgs e)
        {
            _log.Info("Detected deleted package: {0}", e.Name);
            var package = _packageFactory.CreateFromFile(e.Name);
            _scheduler.RemoveJob(package.Name);
        }

        private void AddPackage(string fullPath, string name)
        {
            _log.Info("Detected new package: {0}", name);
            var package = _packageFactory.CreateFromFile(fullPath);
            _scheduler.AddJob(package);
        }

        private IPackageFactory _packageFactory;
        private ILog _log;
        private IScheduler _scheduler;
    }
}
