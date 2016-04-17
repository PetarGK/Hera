using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.EventStore
{
    public class EventStoreConcurrencyException : Exception
    {
        public EventStoreConcurrencyException(IEnumerable<AggregateCommit> storeCommits)
        {
            StoreCommits = storeCommits;
        }

        public IEnumerable<AggregateCommit> StoreCommits { get; private set; }
    }
}
