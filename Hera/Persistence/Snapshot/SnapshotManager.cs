using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using System.IO;
using Hera.Serialization;

namespace Hera.Persistence.Snapshot
{
    public class SnapshotManager : ISnapshotManager
    {
        private readonly ISnapshotStore _snapshotStore;
        private readonly ISerializationManager _serializeManager;

        public SnapshotManager(ISnapshotStore snapshotStore, ISerializationManager serializeManager)
        {
            _snapshotStore = snapshotStore;
            _serializeManager = serializeManager;
        }

        public void CreateSnapshot<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            var snapshot = new Snapshot(aggregateRoot.State.Id.ToString(), aggregateRoot.Revision, _serializeManager.SerializeAggregate<TAggregateRoot>(aggregateRoot));

            _snapshotStore.Save(snapshot);
        }
        public TAggregateRoot RestoreAggregate<TAggregateRoot>(IIdentity aggregateRootId) where TAggregateRoot : IAggregateRoot
        {
            var snapshot = _snapshotStore.Load(aggregateRootId.ToString());
            if (snapshot != null)
            {
                return _serializeManager.DeserializeAggregate<TAggregateRoot>(snapshot.Payload);
            }
            return default(TAggregateRoot);
        }
    }
}
