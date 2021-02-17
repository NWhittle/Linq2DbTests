using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.EntityFrameworkCore;

namespace Linq2DbTests
{
    public class TestService
    {
        private readonly TestDbContext _context;

        public TestService(TestDbContext context)
        {
            _context = context;
        }

        public void UpdateRange<T>(IEnumerable<T> source) where T : class, IEntity
        {
            using var connection = _context.CreateLinqToDbConnection();

            connection.BeginTransaction();

            using var tempTable = connection.CreateTempTable(source, tableName: $"{typeof(T).Name}UpdateTemp");

            var entityTable = connection.GetTable<T>();

            var updates = from e in entityTable
                join te in tempTable on e.Id equals te.Id into gj
                from te in gj
                select te;

            updates.Update(entityTable, e => e);

            connection.CommitTransaction();
        }
    }
}
