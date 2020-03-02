using System;
using Xunit;
using litedbasync;
using System.IO;
using System.Threading.Tasks;

namespace litedbasynctest
{
    public class SimpleDatabaseTest : IDisposable
    {
        private readonly string _databasePath;
        private readonly LiteDatabaseAsync _db;
        public SimpleDatabaseTest()
        {
            _databasePath = Path.Combine(Path.GetTempPath(), "litedbn-async-testing-" + Path.GetRandomFileName() + ".db");
            _db = new LiteDatabaseAsync(_databasePath);
        }

        [Fact]
        public async Task TestCanUpsertAndGetList()
        {
            var collection = _db.GetCollection<Person>();

            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith"
            };

            var upsertResult = await collection.UpsertAsync(person);
            Assert.True(upsertResult);

            var listResult = await collection.QueryAsync().ToListAsync();
            Assert.Single(listResult);
            var resultPerson = listResult[0];
            Assert.Equal(person.Id, resultPerson.Id);
            Assert.Equal(person.FirstName, resultPerson.FirstName);
            Assert.Equal(person.LastName, resultPerson.LastName);
        }

        [Fact]
        public async Task TestCanInsertAndGetList()
        {
            var collection = _db.GetCollection<Person>();

            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith"
            };

            var insertResult = await collection.InsertAsync(person);
            Assert.True(insertResult.IsGuid);
            Assert.Equal(person.Id, insertResult.AsGuid);

            var listResult = await collection.QueryAsync().ToListAsync();
            Assert.Single(listResult);
            var resultPerson = listResult[0];
            Assert.Equal(person.Id, resultPerson.Id);
            Assert.Equal(person.FirstName, resultPerson.FirstName);
            Assert.Equal(person.LastName, resultPerson.LastName);
        }

        [Fact]
        public async Task TestInsertingSameRecordTwiceRaisesException()
        {
            var collection = _db.GetCollection<Person>();

            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith"
            };

            await collection.InsertAsync(person);
            await Assert.ThrowsAnyAsync<LiteAsyncException>(async () => await collection.InsertAsync(person));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {      
                _db.Dispose();
                File.Delete(_databasePath);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
