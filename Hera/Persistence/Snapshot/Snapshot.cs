﻿using Hera.DomainModeling.AggregareRoot;
using Hera.DomainModeling.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence.Snapshot
{
    public class Snapshot
    {
        public Snapshot(string streamId, int revision, byte[] payload)
        {
            StreamId = streamId;
            Revision = revision;
            Payload = payload;
        }
        public string StreamId { get; private set; }
        public int Revision { get; private set; }
        public byte[] Payload { get; private set; }
    }
}
