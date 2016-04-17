using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public static class HeraProjectionExtentions
    {
        public static HeraProjection SetupProjection(this Hera hera)
        {
            return new HeraProjection(hera);
        }

    }
}
