using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hera.Serialization
{
    public class BinarySerializer : ISerialize
    {
        private readonly IFormatter _formatter = new BinaryFormatter();

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
