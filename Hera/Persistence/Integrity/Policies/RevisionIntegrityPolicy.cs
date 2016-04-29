using Hera.Persistence.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Integrity.Policies
{
    public class RevisionIntegrityPolicy : IIntegrityPolicy
    {
        private int? _revision;

        public RevisionIntegrityPolicy()
        {
            _revision = null;
        }

        public bool Check(CommitStream commitStream)
        {
            if (!_revision.HasValue || _revision < commitStream.Revision)
            {
                _revision = commitStream.Revision;
                return true;
            }

            return false;
        }
    }
}
