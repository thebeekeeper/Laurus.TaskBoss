using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;

namespace Laurus.TaskBoss.Core.HttpHandlers
{
    [HttpRequestHandlerAttributes("list")]
    public class ListTasksHandler : HttpRequestHandler
    {
        public ListTasksHandler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public override HttpResponse Handle(HttpRequest httpRequest)
        {
            var jobs = _scheduler.GetJobs();
            var rval = String.Join("<br>", jobs);
            return new HttpResponse(HttpResponseCode.Ok, rval);
        }

        private IScheduler _scheduler;
    }
}
