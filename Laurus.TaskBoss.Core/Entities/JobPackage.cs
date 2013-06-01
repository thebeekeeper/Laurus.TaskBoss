using System.IO;

namespace Laurus.TaskBoss.Core.Entities
{
    public class JobPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DirectoryInfo Location { get; set; }
        public string Executable { get; set; }
        public string CronExpression { get; set; }
        public string WorkingDirectory { get; set; }
    }
}
