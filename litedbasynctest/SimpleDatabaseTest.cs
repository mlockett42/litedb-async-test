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
        public async Task TestCanUpsertAndGetList()
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

            var upsertResult = await collection.UpsertAsync(person);
            Assert.True(upsertResult);

            var listResult = await collection.ToListAsync();
            Assert.Single(listResult);
            var resultPerson = listResult[0];
            Assert.Equal(person.Id, resultPerson.Id);
            Assert.Equal(person.FirstName, resultPerson.FirstName);
            Assert.Equal(person.LastName, resultPerson.LastName);
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

            var insertResult = await collection.InsertAsync(person);
            Assert.True(insertResult.IsGuid);
            Assert.Equal(person.Id, insertResult.AsGuid);

            var listResult = await collection.ToListAsync();
            Assert.Single(listResult);
            var resultPerson = listResult[0];
            Assert.Equal(person.Id, resultPerson.Id);
            Assert.Equal(person.FirstName, resultPerson.FirstName);
            Assert.Equal(person.LastName, resultPerson.LastName);
        }

        [Fact]
        public async Task TestInsertingSameRecordTwiceRaisesException()
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

            await collection.InsertAsync(person);
            await Assert.ThrowsAnyAsync<LiteAsyncException>(async () => await collection.InsertAsync(person));
        }
    }
}
