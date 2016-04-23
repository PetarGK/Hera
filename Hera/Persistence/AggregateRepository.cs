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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Constructors

        public AggregateRepository(IEventStore eventStore, ISnapshotStore snapshotStore, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        public TAggregateRoot Load<TAggregateRoot>(IIdentity aggregateRootId, string bucketId) where TAggregateRoot : IAggregateRoot
        {
            var aggregateRoot = AggregateFactory.CreateAggregateRoot<TAggregateRoot>();

            var stream = _eventStore.Load(aggregateRootId, bucketId);
            aggregateRoot.ReplayEvents(stream.Events, stream.Revision);

            return aggregateRoot;
        }
        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot, string bucketId) where TAggregateRoot : IAggregateRoot
        {
            _unitOfWork.AddEvents(aggregateRoot.State.Id, bucketId, aggregateRoot.Revision, aggregateRoot.UncommittedEvents);

            foreach(IDomainEvent @event in aggregateRoot.UncommittedEvents)
            {
                _eventPublisher.Publish(@event);
            }
        }

        #endregion
    }
}
