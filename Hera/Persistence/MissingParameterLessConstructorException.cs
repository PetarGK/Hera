using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Persistence
{
    public class MissingParameterLessConstructorException : Exception
    {
        public MissingParameterLessConstructorException(Type type)
            : base(string.Format("{0} has no constructor without paramerters. This can be either public or private", type.FullName))
        {
        }
    }
}
