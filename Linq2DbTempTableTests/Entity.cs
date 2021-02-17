namespace Linq2DbTests
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }

        object IEntity.Id
        {
            get => Id;
            set => Id = value != null ? (T) value : default;
        }
    }
}
