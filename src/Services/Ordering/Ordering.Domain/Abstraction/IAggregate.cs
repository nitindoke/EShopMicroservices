using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstraction
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
    }
    //It's special kind of entity that handles the Domain events of the aggregate root, 
    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
