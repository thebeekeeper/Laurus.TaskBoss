﻿using Laurus.TaskBoss.Core.Interfaces;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var jobDetail = JobBuilder.Create()
                .OfType<WindowsExeJob>()
                .UsingJobData("exe_name", package.Executable)
                .UsingJobData("working_dir", package.Location.FullName)
                .Build();
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(package.CronExpression).ForJob(jobDetail).Build();
            _log.Info("Scheduling job with cron expression {0}", package.CronExpression);
            _scheduler.ScheduleJob(jobDetail, trigger);
        }

        void Laurus.TaskBoss.Core.Interfaces.IScheduler.RemoveJob(Entities.JobPackage package)
        {
            _log.Info("Removing job {0}", package.Name);
            // TODO: figure out hwo to delete jobs
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
    }

    public class WindowsExeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var exe = context.MergedJobDataMap.Get("exe_name") as string;
            var wdir = context.MergedJobDataMap.Get("working_dir") as string;
            var startInfo = new ProcessStartInfo(exe);
            startInfo.WorkingDirectory = wdir;
            var startedProcess = System.Diagnostics.Process.Start(startInfo);
            Console.WriteLine(startedProcess.Id);
        }
    }

}
