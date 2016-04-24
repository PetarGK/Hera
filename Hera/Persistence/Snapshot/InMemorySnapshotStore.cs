using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.Identity;
using Hera.Persistence.Snapshot;
using System.Collections.Concurrent;

namespace Hera.Persistence.Snapshot
{
    public class InMemorySnapshotStore : ISnapshotStore
    {
        private readonly ConcurrentDictionary<string, Snapshot> _inMemoryDB = new ConcurrentDictionary<string, Snapshot>();

        public Snapshot Load(string streamId)
        {
            Snapshot snapshot = null;
            _inMemoryDB.TryGetValue(streamId, out snapshot);

            return snapshot;
        }
        public void Save(Snapshot snapshot)
        {
            if (_inMemoryDB.ContainsKey(snapshot.StreamId))
                _inMemoryDB[snapshot.StreamId] = snapshot;
            else
                _inMemoryDB.TryAdd(snapshot.StreamId, snapshot);
        }
    }
}
