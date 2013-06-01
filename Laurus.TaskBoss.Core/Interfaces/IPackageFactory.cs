using Laurus.TaskBoss.Core.Entities;
using System;

namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface IPackageFactory
    {
        JobPackage CreateFromFile(string zipFile);
    }
}
