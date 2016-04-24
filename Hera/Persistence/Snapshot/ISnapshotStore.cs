using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Snapshot
{
    public interface ISnapshotStore
    {
        Snapshot Load(string streamId);
        void Save(Snapshot snapshot);
    }
}
