using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.Identity;
using Hera.Persistence.EventStore;
using System.Collections.Concurrent;
using Hera.DomainModeling.DomainEvent;

namespace Hera.Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentDictionary<string, EventStream> _inMemoryDB = new ConcurrentDictionary<string, EventStream>();

        public void Append(CommitAttempt commitAttempt)
        {
            foreach(var item in commitAttempt.Items)
            {
                EventStream stream;
                _inMemoryDB.TryGetValue(item.StreamId, out stream);
                if (stream == null)
                {
                    stream = new EventStream(new List<IDomainEvent>(item.Events), item.Revision);
                    _inMemoryDB.TryAdd(item.StreamId, stream);
                }
                else
                {
                    if (stream.Revision < item.Revision)
                    {
                        var events = new List<IDomainEvent>(stream.Events);
                        events.AddRange(item.Events);
                        var newStream = new EventStream(events, item.Revision);
                        _inMemoryDB.TryUpdate(item.StreamId, newStream, stream);
                    }
                    else
                    {
                        //int skipEvents = expectedVersion - events.Count();
                        throw new EventStoreConcurrencyException(/*stream.Events.Skip(skipEvents), stream.Version*/);
                    }
                }
            }
        }
        public EventStream Load(string streamId)
        {
            EventStream stream;
            _inMemoryDB.TryGetValue(streamId, out stream);
            return stream != null ? stream : new EventStream(new List<IDomainEvent>(), 0);
        }
        public EventStream Load(string streamId, int skipRevision)
        {
            EventStream stream;
            _inMemoryDB.TryGetValue(streamId, out stream);
            if (stream != null)
                return new EventStream(stream.Events.Skip(skipRevision).ToList(), stream.Revision);
            else
                return new EventStream(new List<IDomainEvent>(), 0);
        }
    }
}
