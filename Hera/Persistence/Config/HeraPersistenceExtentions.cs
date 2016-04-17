﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hera.Persistence.EventStore;

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
            return new HeraPersistence(hera);
        }
    }
}