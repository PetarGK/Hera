using Hera.DomainModeling.DomainEvent;
using Hera.Persistence.EventStore;
using Hera.Persistence.Integrity.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Integrity
{
    public class AggregateIntegrityValidator : IIntegrityValidator
    {
        private List<IIntegrityPolicy> _policies;

        public AggregateIntegrityValidator()
        {
            _policies = new List<IIntegrityPolicy>();
            _policies.Add(new RevisionIntegrityPolicy());
        }

        public bool Validate(EventStream stream)
        {
            foreach(var commit in stream.Commits)
            foreach(var policy in _policies)
            {
                if (!policy.Check(commit))
                    return false; 
            }

            return true;
        }
    }
}
