﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hera.DomainModeling.Identity;
using Hera.Persistence.EventStore;

namespace Hera.Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        public void Append(AggregateCommit aggregateCommit)
        {
            throw new NotImplementedException();
        }
        public CommitStream Load(IIdentity aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}