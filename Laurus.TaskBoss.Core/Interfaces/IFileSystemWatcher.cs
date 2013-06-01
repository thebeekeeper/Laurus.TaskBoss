using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface IFileSystemWatcher
    {
        void WatchDirectory(string path);
    }
}
