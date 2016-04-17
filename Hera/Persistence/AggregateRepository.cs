using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using Hera.Persistence.EventStore;
using Hera.Persistence.Snapshot;

namespace Hera.Persistence
{
    public class AggregateRepository : IAggregateRepository
    {
        #region Fields

        private readonly IEventStore _eventStore;
        private readonly ISnapshotStore _snapshotStore;
        private readonly ICommitNotifier _commitNotifier;

        #endregion

        #region Constructors

        public AggregateRepository(IEventStore eventStore, ISnapshotStore snapshotStore, ICommitNotifier commitNotifier)
        {
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
            _commitNotifier = commitNotifier;
        }

        #endregion

        #region Methods

        public TAggregateRoot Load<TAggregateRoot>(IIdentity aggregateId, string bucketId) where TAggregateRoot : IAggregateRoot
        {
            throw new NotImplementedException();
        }
        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot, string bucketId) where TAggregateRoot : IAggregateRoot
        {
            var aggregateCommit = new AggregateCommit(aggregateRoot.State.Id, bucketId, aggregateRoot.Revision, aggregateRoot.UncommittedEvents);
            _eventStore.Append(aggregateCommit);

            _commitNotifier.Notify();
        }

        #endregion
    }
}
