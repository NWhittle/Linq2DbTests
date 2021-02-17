using Microsoft.EntityFrameworkCore;

namespace Linq2DbTests
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public TestDbContext()
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
