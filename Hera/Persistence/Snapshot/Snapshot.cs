using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Snapshot
{
    public sealed class Snapshot
    {
        public string StreamId { get; set; }
        public int Revision { get; set; }
        public object Payload { get; set; }
    }
}
