using System.Linq;
using uhttpsharp;
using Laurus.TaskBoss.Core.Interfaces;

namespace Laurus.TaskBoss.Core.HttpHandlers
{
    public class StartTaskHandler : HttpRequestHandler
    {
        public StartTaskHandler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public override HttpResponse Handle(HttpRequest httpRequest)
        {
            if (httpRequest.Parameters.Params.Count() > 0)
            {
                var jobName = httpRequest.Parameters.Params[0];
                _scheduler.TriggerJob(jobName);
                return new HttpResponse(HttpResponseCode.Ok, string.Format("Triggered job {0}", jobName));
            }
            else
            {
                return new HttpResponse(HttpResponseCode.BadRequest, "Job name required");
            }
        }

        private readonly IScheduler _scheduler;
    }
}
