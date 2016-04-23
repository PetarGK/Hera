﻿using Hera.DomainModeling.DomainEvent;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public interface IUnitOfWork
    {
        void AddEvents(IIdentity aggregateRootId, string bucketId, int revision, IEnumerable<IDomainEvent> events);
        void Commit();
    }
}