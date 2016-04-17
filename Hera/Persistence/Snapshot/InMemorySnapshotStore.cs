using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.Identity;
using Hera.Persistence.Snapshot;

namespace Hera.Persistence.Snapshot
{
    public class InMemorySnapshotStore : ISnapshotStore
    {
        public Snapshot Load(IIdentity aggregateId)
        {
            throw new NotImplementedException();
        }
        public void Save(Snapshot snapshot)
        {
            throw new NotImplementedException();
        }
    }
}
