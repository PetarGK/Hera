using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using Hera.Persistence.EventStore;
using Hera.Persistence.Snapshot;
using Hera.DomainModeling.Repository;
using Hera.DomainModeling.DomainEvent;

namespace Hera.Persistence
{
    public class AggregateRepository : IAggregateRepository
    {
        #region Fields

        private readonly IEventStore _eventStore;
        private readonly ISnapshotStore _snapshotStore;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Constructors

        public AggregateRepository(IEventStore eventStore, ISnapshotStore snapshotStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        public TAggregateRoot Load<TAggregateRoot>(IIdentity aggregateRootId) where TAggregateRoot : IAggregateRoot
        {
            var aggregateRoot = AggregateFactory.CreateAggregateRoot<TAggregateRoot>();

            EventStream stream = null;
            if (TryRestoreAggregateFromSnapshot(aggregateRootId, aggregateRoot))
                stream = _eventStore.Load(aggregateRootId.ToString(), aggregateRoot.Revision);
            else
                stream = _eventStore.Load(aggregateRootId.ToString());

            var events = new List<IDomainEvent>();
            foreach(object commitEvents in stream.Events)
            {
                events.AddRange(DeserializeEvents(commitEvents));
            }

            aggregateRoot.ReplayEvents(events, stream.Revision);

            return aggregateRoot;
        }
        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            object payload = SerializeEvents(aggregateRoot.UncommittedEvents);

            var commitStream = new CommitStream(aggregateRoot.State.Id.ToString(), aggregateRoot.Revision, payload);

            _eventStore.Append(commitStream);

            foreach (IDomainEvent @event in aggregateRoot.UncommittedEvents)
            {
                _eventPublisher.Publish(@event);
            }
        }

        private bool TryRestoreAggregateFromSnapshot<TAggregateRoot>(IIdentity aggregateRootId, TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            var snapshot = _snapshotStore.Load(aggregateRootId.ToString());
            if (snapshot != null)
            {
                aggregateRoot = DeserializeAggregate<TAggregateRoot>(snapshot.Payload);
                return true;
            }
            return false;
        }

        private object SerializeEvents(IEnumerable<IDomainEvent> events)
        {
            // Use ISerialize interface
            return events;
        }
        private IEnumerable<IDomainEvent> DeserializeEvents(object payload)
        {
            // Use ISerialize interface
            return (IEnumerable<IDomainEvent>)payload;
        }
        private TAggregateRoot DeserializeAggregate<TAggregateRoot>(object payload)
        {
            return (TAggregateRoot)payload;
        }

        #endregion
    }
}
