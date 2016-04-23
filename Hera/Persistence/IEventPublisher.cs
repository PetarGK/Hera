using Hera.DomainModeling.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public interface IEventPublisher
    {
        void Publish(IDomainEvent @event);
    }
}
