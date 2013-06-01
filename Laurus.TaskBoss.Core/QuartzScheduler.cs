using Laurus.TaskBoss.Core.Interfaces;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Core
{
    public class QuartzScheduler : Laurus.TaskBoss.Core.Interfaces.IScheduler
    {
        void Laurus.TaskBoss.Core.Interfaces.IScheduler.AddJob(Entities.JobPackage package)
        {
            var jobDetail = JobBuilder.Create().OfType<WindowsExeJob>().UsingJobData("exe_name", package.Executable).Build();
            ITrigger trigger = TriggerBuilder.Create().StartNow().ForJob(jobDetail).Build();
            //_scheduler.AddJob(jobDetail, true);
            _scheduler.ScheduleJob(jobDetail, trigger);
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.RemoveJob(Entities.JobPackage package)
        {
            throw new NotImplementedException();
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            _scheduler = schedFact.GetScheduler();
            _scheduler.Start();
        }

        Quartz.IScheduler _scheduler;
    }

    public class WindowsExeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var exe = context.MergedJobDataMap.Get("exe_name") as string;
            System.Diagnostics.Process.Start(exe);
        }
    }

}
