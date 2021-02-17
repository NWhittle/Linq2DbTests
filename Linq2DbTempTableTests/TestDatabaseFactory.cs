using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Linq2DbTests
{
    public sealed class TestDatabaseFactory : IDisposable, IAsyncDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private SqliteConnection _connection;

        public async ValueTask DisposeAsync()
        {
            await Task.Run(Dispose);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TestDbContext GenerateDbContext(ITestOutputHelper testOutputHelper = null)
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<DbContext>().UseSqlite(_connection);

            if (testOutputHelper != null)
                optionsBuilder.LogTo(testOutputHelper.WriteLine);

            var context = new TestDbContext(optionsBuilder.Options);

            context.Database.EnsureCreated();

            return context;
        }

        private void ReleaseUnmanagedResources()
        {
            _connection.Dispose();
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();

            if (disposing)
            {
            }
        }

        ~TestDatabaseFactory()
        {
            Dispose(false);
        }
    }
}
