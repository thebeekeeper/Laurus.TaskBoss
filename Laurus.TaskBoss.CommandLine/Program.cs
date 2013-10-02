using Castle.Windsor;
using Laurus.TaskBoss.Core;
using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Castle.MicroKernel.Registration;

namespace Laurus.TaskBoss.CommandLine
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var container = BuildContainer();

			HostFactory.Run(x =>
			{
				x.Service(settings => container.Resolve<ServiceControl>());
				x.RunAsLocalSystem();
				x.SetDescription("Task runner");
				x.SetDisplayName("Laurus.TaskBoss");
				x.SetServiceName("LaurusTaskBoss");
				x.StartAutomatically();
			});
		}

		static IWindsorContainer BuildContainer()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Install(new Installer());
			container.Register(Component.For<ServiceControl>().ImplementedBy<ServiceController>());
			return container;
		}
	}
}
