using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public class CommitNotificationEvent
    {
        public CommitNotificationEvent(string aggregateId, int loadedRevision)
        {
            AggregateId = aggregateId;
            LoadedRevision = loadedRevision;
        }

        public string AggregateId { get; private set; }
        public int LoadedRevision { get; private set; }
    }
}
