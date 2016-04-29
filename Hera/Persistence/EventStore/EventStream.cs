using Hera.DomainModeling;
using Hera.DomainModeling.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.EventStore
{
    public class EventStream
    {
        public EventStream(IEnumerable<CommitStream> commits)
        {
            Commits = commits;
        }

        public IEnumerable<CommitStream> Commits { get; private set; }
        public int Revision
        {
            get { return Commits.Last().Revision; }
        }
    }
}
