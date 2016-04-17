﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public class CommitStream
    {
        public CommitStream(IEnumerable<AggregateCommit> commits)
        {
            Commits = commits;
        }

        public IEnumerable<AggregateCommit> Commits { get; private set; }
    }
}