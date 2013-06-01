using Laurus.TaskBoss.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;

namespace Laurus.TaskBoss.Core
{
    public class HttpListener : IHttpListener
    {
        public HttpListener(ILog log, IHandlerFactory handlerFactory)
        {
            _log = log;
            _handlerFactory = handlerFactory;
        }

        void IHttpListener.Listen(int port)
        {
            HttpServer.Instance.Port = port;
            try
            {
                HttpServer.Instance.StartUp(_handlerFactory);
            }
            catch (Exception e)
            {
                _log.Error("Exception starting http server {0}", e.Message);
            }
        }

        private ILog _log;
        private IHandlerFactory _handlerFactory;
    }
}
