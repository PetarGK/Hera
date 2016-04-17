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

namespace Hera
{
    public static class HeraPersistenceExtentions
    {
        public static HeraPersistence SetupPersistence(this Hera hera)
        {
            return new HeraPersistence(hera);
        }

        public static HeraPersistence UsingInMemoryPersistence(this HeraPersistence hera)
        {
            hera.Builder.RegisterType<InMemoryEventStore>().As<IEventStore>();
            hera.Builder.RegisterType<InMemorySnapshotStore>().As<ISnapshotStore>();
            hera.Builder.RegisterType<DefaultCommitNotifier>().As<ICommitNotifier>();

            hera.Builder.RegisterType<AggregateRepository>().As<IAggregateRepository>();
            return new HeraPersistence(hera);
        }
    }
}
