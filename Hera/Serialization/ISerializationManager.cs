using Hera.DomainModeling.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Serialization
{
    public interface ISerializationManager
    {
        byte[] SerializeEvents(IEnumerable<IDomainEvent> events);
        IEnumerable<IDomainEvent> DeserializeEvents(byte[] payload);

        byte[] SerializeAggregate<TAggregateRoot>(TAggregateRoot aggregateRoot);
        TAggregateRoot DeserializeAggregate<TAggregateRoot>(byte[] payload);
    }
}
