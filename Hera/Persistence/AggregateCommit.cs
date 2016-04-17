using Hera.DomainModeling;
using Hera.DomainModeling.DomainEvent;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public sealed class AggregateCommit
    {
        public AggregateCommit(IIdentity aggregateRootId, string bucketId, int revision, IEnumerable<IDomainEvent> events)
        {
            AggregateRootId = aggregateRootId;
            BucketId = bucketId;
            Revision = revision;
            Events = events;
            Timestamp = DateTime.UtcNow.ToFileTimeUtc();
        }

        public IIdentity AggregateRootId { get; private set; }
        public string BucketId { get; private set; }
        public int Revision { get; private set; }
        public IEnumerable<IDomainEvent> Events { get; private set; }
        public long Timestamp { get; private set; }
    }
}
