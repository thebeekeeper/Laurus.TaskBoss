using System;
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
            if (zipFile.EndsWith(".json"))
            {
                return ReadFromJson(zipFile);
            }

            Manifest manifest = null;
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var directoryInfo = Directory.CreateDirectory(path);
            using (var archive = new ZipArchive(_fileSystem.Read(zipFile)))
            {
                var manifestFile = archive.GetEntry("manifest.json");
                if (manifestFile == default(ZipArchiveEntry))
                {
                    throw new FileNotFoundException(string.Format("Manifest.json not found in {0}", zipFile));
                }
                var manifestContents = new StreamReader(manifestFile.Open()).ReadToEnd();
                manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestContents);
                // BUG: this doesn't handle directories correctly - i don't have the .net 4.5 System.IO.Compression
                // for some reason which has a method to extract everything automatically
                foreach (var e in archive.Entries)
                {
                    var outFile = Path.Combine(path, e.Name);
                    if (!File.Exists(outFile))
                    {
                        var outStream = new FileStream(outFile, FileMode.CreateNew);
                        e.Open().CopyTo(outStream);
                        outStream.Flush();
                        outStream.Close();
                    }
                }
            }

            return new JobPackage()
            {
                Name = manifest.JobName,
                Executable = manifest.Executable,
                Location = directoryInfo,
                CronExpression = manifest.CronExpression,
                WorkingDirectory = manifest.WorkingDirectory,
            };
        }

        private JobPackage ReadFromJson(string jsonFile)
        {
            var manifestContents = new StreamReader(File.OpenRead(jsonFile)).ReadToEnd();
            var manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestContents);
            return new JobPackage()
            {
                Name = manifest.JobName,
                Executable = manifest.Executable,
                CronExpression = manifest.CronExpression,
                WorkingDirectory = manifest.WorkingDirectory,
				Location = new DirectoryInfo(manifest.WorkingDirectory),
            };
        }

        private IFileSystem _fileSystem;
    }
}
