using uhttpsharp;
using Laurus.TaskBoss.Core.Interfaces;
using System.Text;

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
			var sb = new StringBuilder();
			foreach (var j in jobs)
			{
				sb.AppendFormat("<a href=\"/run/{0}\">{0}</a><br/>", j);
			}

			//var rval = string.Join("<br>", jobs);
			var rval = sb.ToString();
			return new HttpResponse(HttpResponseCode.Ok, rval);
		}

		private IScheduler _scheduler;
	}
}
