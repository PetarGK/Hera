using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public static class HeraSnapshottingExtentions
    {
        public static HeraSnapshotting SetupSnapshotting(this Hera hera)
        {
            return new HeraSnapshotting(hera);
        }
    }
}
