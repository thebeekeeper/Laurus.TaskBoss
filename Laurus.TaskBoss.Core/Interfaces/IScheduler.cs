using System.Collections.Generic;
using Laurus.TaskBoss.Core.Entities;

namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface IScheduler
    {
        void AddJob(JobPackage package);
        void RemoveJob(string name);
        void Start();
        void TriggerJob(string jobId);
        IEnumerable<string> GetJobs();
    }
}
