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
        private readonly ISerialize _serialize;

        public SnapshotManager(ISnapshotStore snapshotStore, ISerialize serialize)
        {
            _snapshotStore = snapshotStore;
            _serialize = serialize;
        }

        public void CreateSnapshot<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            var snapshot = new Snapshot(aggregateRoot.State.Id.ToString(), aggregateRoot.Revision, SerializeAggregate<TAggregateRoot>(aggregateRoot));

            _snapshotStore.Save(snapshot);
        }
        public TAggregateRoot RestoreAggregate<TAggregateRoot>(IIdentity aggregateRootId) where TAggregateRoot : IAggregateRoot
        {
            var snapshot = _snapshotStore.Load(aggregateRootId.ToString());
            if (snapshot != null)
            {
                return DeserializeAggregate<TAggregateRoot>(snapshot.Payload);
            }
            return default(TAggregateRoot);
        }

        private byte[] SerializeAggregate<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            using (var stream = new MemoryStream())
            {
                _serialize.Serialize<TAggregateRoot>(stream, aggregateRoot);
                return stream.ToArray();
            }
        }
        private TAggregateRoot DeserializeAggregate<TAggregateRoot>(byte[] payload)
        {
            using (var stream = new MemoryStream(payload))
            {
                return _serialize.Deserialize<TAggregateRoot>(stream);
            }
        }

    }
}
