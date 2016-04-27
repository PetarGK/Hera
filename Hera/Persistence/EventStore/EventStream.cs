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

        private IEnumerable<CommitStream> Commits { get; set; }

        public IEnumerable<object> Events
        {
            get { return Commits.SelectMany(s => (IEnumerable<object>)s.Payload).ToList(); }
        }
        public int Revision
        {
            get { return Commits.Last().Revision; }
        }
    }
}
