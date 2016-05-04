using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hera.Persistence.EventStore;
using Hera.Persistence;
using Hera.Persistence.Snapshot;
using Hera.DomainModeling.Repository;
using Hera.Persistence.Integrity;

namespace Hera
{
    public static class HeraPersistenceExtentions
    {
        public static HeraPersistence SetupPersistence(this Hera hera)
        {
            hera.Builder.RegisterType<AggregateRepository>().As<IAggregateRepository>().InstancePerLifetimeScope();
            hera.Builder.RegisterType<AggregateIntegrityValidator>().As<IIntegrityValidator>().InstancePerLifetimeScope();
            hera.Builder.RegisterType<SnapshotManager>().As<ISnapshotManager>().InstancePerLifetimeScope();

            return new HeraPersistence(hera);
        }

        public static HeraPersistence UsingInMemoryPersistence(this HeraPersistence hera)
        {
            hera.Builder.RegisterType<InMemoryEventStore>().As<IEventStore>().SingleInstance();
            hera.Builder.RegisterType<InMemorySnapshotStore>().As<ISnapshotStore>().SingleInstance();
            

            // TODO: Move somewhere else. This is not the right place.
            hera.Builder.RegisterType<DefaultCommitNotifier>().As<ICommitNotifier>().SingleInstance();
            hera.Builder.RegisterType<DefaultEventPublisher>().As<IEventPublisher>().SingleInstance();
            
            return new HeraPersistence(hera);
        }
    }
}
