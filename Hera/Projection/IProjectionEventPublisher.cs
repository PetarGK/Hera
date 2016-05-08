using Hera.DomainModeling.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Projection
{
    public interface IProjectionEventPublisher
    {
        void Publish(IDomainEvent @event);
    }
}
