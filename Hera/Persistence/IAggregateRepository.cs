using Hera.DomainModeling.AggregareRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public interface IAggregateRepository
    {
        void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot;
        TAggregateRoot Load<TAggregateRoot>(Guid id) where TAggregateRoot : IAggregateRoot;
    }
}
