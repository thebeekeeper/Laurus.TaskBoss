using uhttpsharp;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Laurus.TaskBoss.Core.HttpHandlers;
using Laurus.TaskBoss.Core.Interfaces;

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
            container.Register(Component.For<IHttpListener>().ImplementedBy<HttpListener>().LifestyleSingleton());

            container.Register(Component.For<HttpRequestHandler>().Named("run").ImplementedBy<StartTaskHandler>());
            container.Register(Component.For<HttpRequestHandler>().Named("list").ImplementedBy<ListTasksHandler>());
            container.Register(Component.For<IHandlerFactory>().ImplementedBy<WindsorHandlerFactory>());
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
        }
    }
}
