using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.DomainEvent;
using System.IO;

namespace Hera.Serialization
{
    public class SerializationManager : ISerializationManager
    {
        private readonly ISerialize _serialize;

        public SerializationManager(ISerialize serialize)
        {
            _serialize = serialize;
        }

        public byte[] SerializeEvents(IEnumerable<IDomainEvent> events)
        {
            using (var stream = new MemoryStream())
            {
                _serialize.Serialize(stream, events);
                return stream.ToArray();
            }
        }
        public IEnumerable<IDomainEvent> DeserializeEvents(byte[] payload)
        {
            using (var stream = new MemoryStream(payload))
            {
                return _serialize.Deserialize<IEnumerable<IDomainEvent>>(stream);
            }
        }

        public byte[] SerializeAggregate<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            using (var stream = new MemoryStream())
            {
                _serialize.Serialize(stream, aggregateRoot);
                return stream.ToArray();
            }
        }
        public TAggregateRoot DeserializeAggregate<TAggregateRoot>(byte[] payload)
        {
            using (var stream = new MemoryStream(payload))
            {
                return _serialize.Deserialize<TAggregateRoot>(stream);
            }
        }
    }
}
