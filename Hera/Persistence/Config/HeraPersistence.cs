using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public class HeraPersistence : Hera
    {
        public HeraPersistence(Hera inner)
            : base(inner)
        {
        }
    }
}
