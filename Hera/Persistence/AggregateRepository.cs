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
using Hera.Persistence.Integrity;
using Hera.Serialization;
using System.IO;

namespace Hera.Persistence
{
    public class AggregateRepository : IAggregateRepository
    {
        #region Fields

        private readonly IEventStore _eventStore;
        private readonly ISnapshotManager _snapshotManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly IIntegrityValidator _integrityValidator;
        private readonly ISerialize _serialize;

        #endregion

        #region Constructors

        public AggregateRepository(IEventStore eventStore, 
                                   ISnapshotManager snapshotManager, 
                                   IEventPublisher eventPublisher, 
                                   IIntegrityValidator integrityValidator,
                                   ISerialize serialize)
        {
            _eventStore = eventStore;
            _snapshotManager = snapshotManager;
            _eventPublisher = eventPublisher;
            _integrityValidator = integrityValidator;
            _serialize = serialize;
        }

        #endregion

        #region Methods

        public TAggregateRoot Load<TAggregateRoot>(IIdentity aggregateRootId) where TAggregateRoot : IAggregateRoot
        {
            TAggregateRoot aggregateRoot = default(TAggregateRoot);
            aggregateRoot = _snapshotManager.RestoreAggregate<TAggregateRoot>(aggregateRootId);

            EventStream stream = null;
            if (aggregateRoot == null)
            {
                aggregateRoot = AggregateFactory.CreateAggregateRoot<TAggregateRoot>();
                stream = _eventStore.Load(aggregateRootId.ToString());
            }
            else
            {
                stream = _eventStore.Load(aggregateRootId.ToString(), aggregateRoot.Revision);
            }                

            if (!_integrityValidator.Validate(stream))
                throw new InvalidAggregateIntegrityException();

            var events = new List<IDomainEvent>();
            foreach(CommitStream commit in stream.Commits)
                events.AddRange(DeserializeEvents(commit.Payload));

            aggregateRoot.ReplayEvents(events, stream.Revision);

            return aggregateRoot;
        }
        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            byte[] payload = SerializeEvents(aggregateRoot.UncommittedEvents);
            var commitStream = new CommitStream(aggregateRoot.State.Id.ToString(), aggregateRoot.Revision, payload);

            _eventStore.Append(commitStream);
            foreach (IDomainEvent @event in aggregateRoot.UncommittedEvents)
            {
                _eventPublisher.Publish(@event);
            }
        }

        private byte[] SerializeEvents(IEnumerable<IDomainEvent> events)
        {
            using (var stream = new MemoryStream())
            {
                _serialize.Serialize<IEnumerable<IDomainEvent>>(stream, events);
                return stream.ToArray();
            }
        }
        private IEnumerable<IDomainEvent> DeserializeEvents(byte[] payload)
        {
            using (var stream = new MemoryStream(payload))
            {
                return _serialize.Deserialize<IEnumerable<IDomainEvent>>(stream);
            }
        }

        #endregion
    }
}
