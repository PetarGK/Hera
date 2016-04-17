using Hera.DomainModeling;
using Hera.DomainModeling.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public class CommitStream
    {
        public CommitStream(IEnumerable<AggregateCommit> commits)
        {
            Commits = commits;
        }

        private IEnumerable<AggregateCommit> Commits { get; set; }

        public IEnumerable<IDomainEvent> Events
        {
            get { return Commits.SelectMany(s => s.Events).ToList(); }
        }
        public int Revision
        {
            get { return Commits.Last().Revision; }
        }
    }
}
