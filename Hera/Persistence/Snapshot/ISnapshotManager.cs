using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Snapshot
{
    public interface ISnapshotManager
    {
        void CreateSnapshot<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot;
        TAggregateRoot RestoreAggregate<TAggregateRoot>(IIdentity aggregateRootId) where TAggregateRoot : IAggregateRoot;
    }
}
