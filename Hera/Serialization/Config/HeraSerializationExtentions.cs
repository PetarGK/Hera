using Autofac;
using Hera.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public static class HeraSerializationExtentions
    {
        public static HeraSerialization UsingBinarySerialization(this HeraPersistence hera)
        {
            hera.Builder.RegisterType<BinarySerializer>().As<ISerialize>().InstancePerLifetimeScope();

            return new HeraSerialization(hera);
        }
    }
}
