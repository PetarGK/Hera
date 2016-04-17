using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public interface IAggregateRepository
    {
        void Save<TAggregateRoot>(TAggregateRoot aggregateRoot, string bucketId) where TAggregateRoot : IAggregateRoot;
        TAggregateRoot Load<TAggregateRoot>(IIdentity aggregateId, string bucketId) where TAggregateRoot : IAggregateRoot;
    }
}
