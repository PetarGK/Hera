using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.DomainEvent;
using Hera.DomainModeling.Identity;
using Hera.Persistence.EventStore;

namespace Hera.Persistence
{
    public class EventSourcedUnitOfWork : IUnitOfWork
    {
        private readonly IEventStore _eventStore;
        private readonly List<IAggregateRoot> _aggregates;

        public EventSourcedUnitOfWork(IEventStore eventStore)
        {
            _eventStore = eventStore;
            _aggregates = new List<IAggregateRoot>();
        }

        public void Append(IAggregateRoot aggregateRoot)
        {
            _aggregates.Add(aggregateRoot);
        }
        public void Commit()
        {
            var commitAttempt = new CommitAttempt();

            foreach(var aggregate in _aggregates)
            {
                commitAttempt.AddAggregate(aggregate.State.Id.ToString(), aggregate.Revision, aggregate.UncommittedEvents);
            }

            _eventStore.Append(commitAttempt);
        }
    }
}
