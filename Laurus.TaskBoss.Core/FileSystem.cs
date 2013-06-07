using System;
using System.IO;
using Laurus.TaskBoss.Core.Interfaces;

namespace Laurus.TaskBoss.Core
{
    public class FileSystem : IFileSystem
    {
        string[] IFileSystem.ReadText(string path)
        {
            return File.ReadAllLines(path);
        }

        Stream IFileSystem.Read(string path)
        {
            return File.OpenRead(path);
        }

    }
}
