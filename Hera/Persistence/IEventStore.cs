using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public interface IEventStore
    {
        void Append(AggregateCommit aggregateCommit);
        EventStream Load(Guid aggregateId);
    }
}
