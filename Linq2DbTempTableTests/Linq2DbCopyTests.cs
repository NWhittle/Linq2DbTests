using System.Collections.Generic;
using FluentAssertions;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Xunit;

namespace Linq2DbTests
{
    public class Linq2DbCopyTests
    {
        [Fact]
        public void Bulk_Copy_Creates_Ids_When_Inserting()
        {
            var factory = new TestDatabaseFactory();
            var context = factory.GenerateDbContext();

            var source = new List<Person>
            {
                new Person {FirstName = "Jane", LastName = "2"},
                new Person {FirstName = "Joe", LastName = "3"}
            };

            using var connection = context.CreateLinqToDbConnection();

            var table = connection.GetTable<Person>();

            table.BulkCopy(source);

            context.People.Should().BeEquivalentTo(source, options => options.Excluding(p => p.Id));
        }
    }
}
