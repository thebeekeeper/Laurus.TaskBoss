using Laurus.TaskBoss.Core.Entities;

namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface IPackageFactory
    {
        JobPackage CreateFromFile(string zipFile);
    }
}
