using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Laurus.TaskBoss.Core.UnitTest
{
    public class FakeTests
    {
        [Fact]
        public void FakeTest1()
        {
            Assert.True(true);
        }

        [Fact]
        public void FakeTest2()
        {
            Assert.True(false);
        }

        [Fact]
        public void FakeTest3()
        {
            Assert.Equal(9, 9);
        }
    }
}
