using uhttpsharp;
using Laurus.TaskBoss.Core.Interfaces;

namespace Laurus.TaskBoss.Core.HttpHandlers
{
    public class ListTasksHandler : HttpRequestHandler
    {
        public ListTasksHandler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public override HttpResponse Handle(HttpRequest httpRequest)
        {
            var jobs = _scheduler.GetJobs();
            var rval = string.Join("<br>", jobs);
            return new HttpResponse(HttpResponseCode.Ok, rval);
        }

        private IScheduler _scheduler;
    }
}
