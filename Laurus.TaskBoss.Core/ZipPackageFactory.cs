using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Laurus.TaskBoss.Core.Entities;
using Laurus.TaskBoss.Core.Interfaces;

namespace Laurus.TaskBoss.Core
{
    public class ZipPackageFactory : IPackageFactory
    {
        public ZipPackageFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        Entities.JobPackage IPackageFactory.CreateFromFile(string zipFile)
        {
            Manifest manifest = null;
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var directoryInfo = Directory.CreateDirectory(path);
            using (var archive = new ZipArchive(_fileSystem.Read(zipFile)))
            {
                var manifestFile = archive.GetEntry("manifest.json");
                if (manifestFile == default(ZipArchiveEntry))
                {
                    throw new FileNotFoundException(String.Format("Manifest.json not found in {0}", zipFile));
                }
                var manifestContents = new StreamReader(manifestFile.Open()).ReadToEnd();
                manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestContents);
                foreach (var e in archive.Entries)
                {
                    var outStream = new FileStream(Path.Combine(path, e.Name), FileMode.CreateNew);
                    e.Open().CopyTo(outStream);
                }
            }

            return new JobPackage()
            {
                Name = manifest.JobName,
                Executable = manifest.Executable,
                Location = directoryInfo,
                CronExpression = manifest.CronExpression,
            };
        }

        private IFileSystem _fileSystem;
    }
}
