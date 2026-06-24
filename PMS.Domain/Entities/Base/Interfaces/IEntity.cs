namespace PMS.Domain.Entities.Base.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
