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

        public void Append(CommitStream commitStream)
        {

        }
        public EventStream Load(string streamId)
        {
            return null;
        }
        public EventStream Load(string streamId, int skipRevision)
        {
            return null;
        }
    }
}
