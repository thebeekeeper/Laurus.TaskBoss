using Laurus.TaskBoss.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Laurus.TaskBoss.Core.UnitTest
{
    public class SchedulerTests
    {
        [Fact]
        public void AddScheduledJob_Should_AddJob()
        {
            var jobName = Guid.NewGuid().ToString();
            var logMock = new Mock<ILog>();
            IScheduler scheduler = new QuartzScheduler(logMock.Object);
            scheduler.Start();

            scheduler.AddJob(new Entities.JobPackage() 
            {
                Name = jobName,
                Location = new System.IO.DirectoryInfo(System.IO.Path.GetTempPath()),
                CronExpression = "0 0 12 ? * WED",
            });

            var jobNames = scheduler.GetJobs();
            Assert.Equal(jobName, jobNames.First());
        }

        [Fact]
        public void Remove_Should_Remove_Existing_Job()
        {
            var jobName = Guid.NewGuid().ToString();
            var logMock = new Mock<ILog>();
            IScheduler scheduler = new QuartzScheduler(logMock.Object);
            scheduler.Start();
            var package = new Entities.JobPackage() 
            {
                Name = jobName,
                Location = new System.IO.DirectoryInfo(System.IO.Path.GetTempPath()),
                CronExpression = "0 0 12 ? * WED",
            };
            scheduler.AddJob(package);

            scheduler.RemoveJob(package);

            var jobNames = scheduler.GetJobs();
            Assert.Equal(0, jobNames.Count());
        }

        [Fact]
        public void Cannot_Remove_Job_That_Doesnt_Exist()
        {
            var jobName = Guid.NewGuid().ToString();
            var logMock = new Mock<ILog>();
            IScheduler scheduler = new QuartzScheduler(logMock.Object);
            scheduler.Start();
            var package = new Entities.JobPackage() 
            {
                Name = jobName,
                Location = new System.IO.DirectoryInfo(System.IO.Path.GetTempPath()),
                CronExpression = "0 0 12 ? * WED",
            };

            scheduler.RemoveJob(package);

            var jobNames = scheduler.GetJobs();
            Assert.False(jobNames.Contains(jobName));
        }

        [Fact]
        public void Cannot_Add_Duplicate_Jobs()
        {
            var jobName = Guid.NewGuid().ToString();
            var logMock = new Mock<ILog>();
            IScheduler scheduler = new QuartzScheduler(logMock.Object);
            scheduler.Start();
            var package = new Entities.JobPackage()
            {
                Name = jobName,
                Location = new System.IO.DirectoryInfo(System.IO.Path.GetTempPath()),
                CronExpression = "0 0 12 ? * WED",
            };

            scheduler.AddJob(package);
            scheduler.AddJob(package);

            var jobNames = scheduler.GetJobs();
            Assert.Equal(1, jobNames.Count(x => x.Equals(jobName)));

        }
    }
}
