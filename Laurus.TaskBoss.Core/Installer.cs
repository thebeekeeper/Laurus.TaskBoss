using Castle.MicroKernel.Registration;
using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Core
{
    public class Installer : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IFileSystem>().ImplementedBy<FileSystem>());
            container.Register(Component.For<IFileSystemWatcher>().ImplementedBy<FileSystemWatcher>().LifestyleSingleton());
            container.Register(Component.For<ILog>().ImplementedBy<Logger>().LifestyleSingleton());
            container.Register(Component.For<IScheduler>().ImplementedBy<QuartzScheduler>().LifestyleSingleton());
            container.Register(Component.For<IPackageFactory>().ImplementedBy<ZipPackageFactory>());
        }
    }
}
