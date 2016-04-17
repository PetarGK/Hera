using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.EventStore
{
    public interface IEventStore
    {
        void Append(AggregateCommit aggregateCommit);
        CommitStream Load(IIdentity aggregateId);
    }
}
