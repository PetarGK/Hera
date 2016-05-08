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

namespace Hera.Persistence
{
    public class AggregateRepository : IAggregateRepository
    {
        #region Fields

        private readonly IEventStore _eventStore;
        private readonly ISnapshotManager _snapshotManager;
        private readonly ISerializationManager _serializeManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICommitNotifier _commitNotifier;
        private readonly IIntegrityValidator _integrityValidator;
        
        private readonly static Dictionary<IIdentity, AggregateMetadata> _loadedAggregates;
        private readonly static object _syncObject;

        #endregion

        #region Constructors

        static AggregateRepository()
        {
            _loadedAggregates = new Dictionary<IIdentity, AggregateMetadata>();
            _syncObject = new object();
        }

        public AggregateRepository(IEventStore eventStore, 
                                   ISnapshotManager snapshotManager,
                                   ISerializationManager serializeManager,
                                   IEventPublisher eventPublisher,
                                   ICommitNotifier commitNotifier,
                                   IIntegrityValidator integrityValidator)
        {
            _eventStore = eventStore;
            _snapshotManager = snapshotManager;
            _serializeManager = serializeManager;
            _eventPublisher = eventPublisher;
            _commitNotifier = commitNotifier;
            _integrityValidator = integrityValidator;
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
                events.AddRange(_serializeManager.DeserializeEvents(commit.Payload));

            aggregateRoot.ReplayEvents(events, stream.Revision);

            StoreRevision(aggregateRootId, aggregateRoot.Revision);

            return aggregateRoot;
        }
        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            byte[] payload = _serializeManager.SerializeEvents(aggregateRoot.UncommittedEvents);
            var commitStream = new CommitStream(aggregateRoot.State.Id.ToString(), aggregateRoot.Revision, payload);

            _eventStore.Append(commitStream);
            foreach (IDomainEvent @event in aggregateRoot.UncommittedEvents)
            {
                _eventPublisher.Publish(@event);
            }

            _commitNotifier.Notify(new CommitNotificationEvent(aggregateRoot.State.Id.ToString(), GetRevision(aggregateRoot.State.Id)));

            RemoveRevision(aggregateRoot.State.Id);
        }

        private void StoreRevision(IIdentity id, int revision)
        {
            lock(_syncObject)
            {
                if (!_loadedAggregates.ContainsKey(id))
                {
                    _loadedAggregates[id] = new AggregateMetadata() { Revision = revision, Count = 1 };
                }
                else
                {
                    var meta = _loadedAggregates[id];
                    meta.Count++;
                    if (meta.Revision < revision)
                        meta.Revision = revision;
                }
            }
        }
        private void RemoveRevision(IIdentity id)
        {
            lock (_syncObject)
            {
                var meta = _loadedAggregates[id];
                meta.Count--;

                if (meta.Count == 0)
                {
                    _loadedAggregates.Remove(id);
                }
            }
        }
        private int GetRevision(IIdentity id)
        {
            lock (_syncObject)
            {
                return _loadedAggregates[id].Revision;
            }
        }

        #endregion

        class AggregateMetadata
        {
            public int Revision { get; set; }
            public int Count { get; set; }
        }
    }
}
