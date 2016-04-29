using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Serialization
{
    public class JsonSerializer : ISerialize
    {


        public T Deserialize<T>(Stream input)
        {
            throw new NotImplementedException();
        }
        public void Serialize<T>(Stream output, T graph)
        {
            throw new NotImplementedException();
        }
    }
}
