using System;
using Xunit;
using litedbasync;
using System.IO;
using System.Threading.Tasks;

namespace litedbasynctest
{
    public class SimpleDatabaseTest
    {
        [Fact]
        public void TestCanOpenDatabase()
        {
            string databasePath = Path.Combine(Path.GetTempPath(), "litedbn-async-testing-" + Path.GetRandomFileName() + ".db");
            var db = new LiteDatabaseAsync(databasePath);
        }

        [Fact]
        public async Task TestCanInsertAndGetList()
        {
            string databasePath = Path.Combine(Path.GetTempPath(), "litedb-async-testing-" + Path.GetRandomFileName() + ".db");
            var db = new LiteDatabaseAsync(databasePath);
            var collection = db.GetCollection<Person>();

            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith"
            };

            await collection.UpsertAsync(person);

            var result = await collection.ToListAsync();
            Assert.Single(result);
            var resultPerson = result[0];
            Assert.Equal(person.Id, resultPerson.Id);
            Assert.Equal(person.FirstName, resultPerson.FirstName);
            Assert.Equal(person.LastName, resultPerson.LastName);
        }
    }
}
