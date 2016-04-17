using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public class HeraSnapshotting : Hera
    {
        public HeraSnapshotting(Hera inner)
            : base(inner)
        {
        }
    }
}
