namespace Linq2DbTests
{
    public class Person : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
