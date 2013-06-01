using System;
using System.IO;

namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface IFileSystem
    {
        string[] ReadText(string path);
        Stream Read(string path);
    }
}
