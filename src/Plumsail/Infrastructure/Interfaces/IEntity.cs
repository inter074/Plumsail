namespace Infrastructure.Interfaces
{
    public interface IEntity<out TEntity>
    {
        TEntity Id { get; }
    }
}