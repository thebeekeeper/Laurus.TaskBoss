using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface ILog
    {
        void Debug(string message, params object[] items);
        void Info(string message, params object[] items);
        void Error(string message, params object[] items);
        void Fatal(string message, params object[] items);
    }
}
