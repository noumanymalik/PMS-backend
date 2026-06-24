using PMS.Domain.Entities.Base.Interfaces;

namespace PMS.Domain.Entities.Base
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default;
    }
}
