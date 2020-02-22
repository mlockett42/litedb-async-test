using System;
using Xunit;
using litedbasync;
using System.IO;

namespace litedbasynctest
{
    public class SimpleDatabaseTest
    {
        [Fact]
        public void TestCanOpenDatabase()
        {
            string databasePath = Path.Combine(Path.GetTempPath(), "litednasyn-testing-" + Path.GetRandomFileName() + ".db");
            var db = new LiteDatabaseAsync(databasePath);
        }
    }
}
