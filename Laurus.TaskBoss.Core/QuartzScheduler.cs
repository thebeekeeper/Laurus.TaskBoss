using System.Collections.Generic;
using System.Linq;
using Laurus.TaskBoss.Core.Interfaces;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace Laurus.TaskBoss.Core
{
    public class QuartzScheduler : Laurus.TaskBoss.Core.Interfaces.IScheduler
    {
        public QuartzScheduler(ILog log)
        {
            _log = log;
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.AddJob(Entities.JobPackage package)
        {
            if (_scheduler.CheckExists(new JobKey(package.Name)))
            {
                _log.Error("Package named {0} already exists - not scheduling this one", package.Name);
                return;
            }
            string workingDir = package.Location.FullName;

            var jobDetail = JobBuilder.Create()
                .OfType<WindowsExeJob>()
                .UsingJobData("exe_name", package.Executable)
                .UsingJobData("working_dir", workingDir)
                .WithIdentity(package.Name)
                .Build();
            _packageCount++;
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(package.CronExpression).ForJob(jobDetail).Build();
            _log.Info("Scheduling job with cron expression {0}", package.CronExpression);
            _scheduler.ScheduleJob(jobDetail, trigger);
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.RemoveJob(string name)
        {
            _log.Info("Removing job {0}", name);
            var key = new JobKey(name);
            if (_scheduler.CheckExists(key) == false)
            {
                _log.Error("Cannot remove job [{0}] because it doesn't exist", name);
            }
            else
            {
                _scheduler.DeleteJob(key);
            }
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            _scheduler = schedFact.GetScheduler();
            _scheduler.Start();
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.TriggerJob(string jobId)
        {
            var key = new JobKey(jobId);
            _scheduler.TriggerJob(key);
        }

        IEnumerable<string> Laurus.TaskBoss.Core.Interfaces.IScheduler.GetJobs()
        {
            var groupNames = _scheduler.GetJobGroupNames();
            var jobNames = _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupContains("DEFAULT")).Select(k => k.Name);
            return jobNames;
        }

        private Quartz.IScheduler _scheduler;
        private ILog _log;
        private int _packageCount = 0;
    }

}
