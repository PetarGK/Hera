using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public interface ICommitNotifier
    {
        void Notify();
    }

    public class DefaultCommitNotifier : ICommitNotifier
    {
        public void Notify()
        {
            
        }
    }
}
