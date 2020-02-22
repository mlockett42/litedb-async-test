using System;
using Xunit;
using litedbasync;

namespace litedbasynctest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var v = new litedbasync.Class1();
            Assert.Equal(v.Hello(), "World");
        }
    }
}
