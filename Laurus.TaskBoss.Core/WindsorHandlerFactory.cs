using uhttpsharp;
using Castle.Windsor;

namespace Laurus.TaskBoss.Core
{
    public class WindsorHandlerFactory : IHandlerFactory
    {
        // TODO: refactor this to not be a service locator
        public WindsorHandlerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        HttpRequestHandler IHandlerFactory.GetHandler(string name)
        {
            if (_container.Kernel.HasComponent(name))
            {
                return _container.Resolve<HttpRequestHandler>(name);
            }
            else
            {
                return null;
            }
        }

        private IWindsorContainer _container;
    }
}
