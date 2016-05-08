using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.Persistence;
using Hera.Persistence.EventStore;
using Hera.Serialization;

namespace Hera.Projection
{
    public class PollingClient : IPollingClient
    {
        private readonly IEventStore _eventStore;
        private readonly ISerializationManager _serializeManager;
        private readonly IProjectionEventPublisher _eventPublisher;

        public PollingClient(IEventStore eventStore, ISerializationManager serializeManager, IProjectionEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _serializeManager = serializeManager;
            _eventPublisher = eventPublisher;
        }

        public void Poll(CommitNotificationEvent @event)
        {
            var eventStream = _eventStore.Load(@event.AggregateId, @event.LoadedRevision);

            foreach(var commit in eventStream.Commits)
            {
                var domainEvents = _serializeManager.DeserializeEvents(commit.Payload);
                foreach(var domainEvent in domainEvents)
                {
                    _eventPublisher.Publish(domainEvent);
                }
            }
        }
    }
}
