using Hera.DomainModeling;
using Hera.DomainModeling.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.EventStore
{
    public class EventStream
    {
        public EventStream(IEnumerable<IDomainEvent> events, int revision)
        {
            Events = events;
            Revision = revision;
        }

        public IEnumerable<IDomainEvent> Events { get; private set; }
        public int Revision { get; private set; }
    }
}
