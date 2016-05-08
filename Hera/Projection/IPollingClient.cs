using Hera.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Projection
{
    public interface IPollingClient
    {
        void Poll(CommitNotificationEvent @event);
    }
}
