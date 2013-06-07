using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Service
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[] 
            { 
                new TaskBossService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
