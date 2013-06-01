using Laurus.TaskBoss.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Laurus.TaskBoss.Core.UnitTest
{
    public class ZipPackageFactoryTests
    {
        [Fact]
        public void WriteMoreTests()
        {
            var fileSystemMock = new Mock<IFileSystem>(MockBehavior.Strict);
            var stringStream = "{\"JobName\": \"sample\",\"Executable\": \"Notepad.exe\"}".ToStream();
            fileSystemMock.Setup(a => a.Read("test.zip")).Returns(stringStream);

            IPackageFactory packageFactory = new ZipPackageFactory(fileSystemMock.Object);
            var package = packageFactory.CreateFromFile("test.zip");

            Assert.Equal("sample", package.Name);
        }
    }

    public static class StringExtensions
    {
        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
