using Hera.Persistence.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Integrity
{
    public interface IIntegrityPolicy
    {
        bool Check(CommitStream commitStream);
    }
}
