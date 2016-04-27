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
        void Append(CommitStream commitStream);
        EventStream Load(string streamId);
        EventStream Load(string streamId, int skipRevision);
    }
}
