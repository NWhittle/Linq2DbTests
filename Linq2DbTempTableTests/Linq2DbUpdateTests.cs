using System.Collections.Generic;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Linq2DbTests
{
    public class Linq2DbUpdateTests
    {
        [Fact]
        public void Bulk_Update_Updates_All_Records()
        {
            var factory = new TestDatabaseFactory();
            var context = factory.GenerateDbContext();

            var source = new List<Person>
            {
                new Person {FirstName = "Jane", LastName = "Doe"},
                new Person {FirstName = "Joe", LastName = "Doe"}
            };

            context.UpdateRange(source);

            context.SaveChanges();

            source.ForEach(p =>
            {
                p.FirstName = "John";
                context.Entry(p).State = EntityState.Detached;
            });

            var testService = new TestService(context);

            testService.UpdateRange(source);

            context.People.Should().BeEquivalentTo(source);
        }
    }
}
