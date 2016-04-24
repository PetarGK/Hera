using Hera.DomainModeling;
using Hera.DomainModeling.DomainEvent;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.EventStore
{
    public sealed class CommitAttempt
    {
        public CommitAttempt()
        {
            Items = new List<CommitAttemptItem>();
        }

        public void AddAggregate(string streamId, int revision, IEnumerable<IDomainEvent> events)
        {
            Items.Add(new CommitAttemptItem(streamId, revision, events));
        }

        public List<CommitAttemptItem> Items { get; private set; }
    }

    public sealed class CommitAttemptItem
    {
        public CommitAttemptItem(string streamId, int revision, IEnumerable<IDomainEvent> events)
        {
            StreamId = streamId;
            Revision = revision;
            Events = events;
        }

        public string StreamId { get; private set; }
        public int Revision { get; private set; }
        public IEnumerable<IDomainEvent> Events { get; private set; }
    }
}
