using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hera
{
    public class Hera
    {
        #region Fields

        private readonly ContainerBuilder _builder;
        private readonly Hera _inner;

        #endregion

        #region Constructors

        protected Hera(ContainerBuilder builder)
        {
            _builder = builder;
        }
        protected Hera(Hera inner)
        {
            _inner = inner;
        }

        #endregion

        #region Properties

        public ContainerBuilder Builder
        {
            get { return _builder ?? _inner.Builder; }
        }

        #endregion

        #region Methods

        public static Hera Init(ContainerBuilder builder)
        {
            //container.Register(TransactionScopeOption.Suppress);
            //container.Register<IPersistStreams>(new InMemoryPersistenceEngine());
            //container.Register<IScheduleDispatches>(new NullDispatcher());
            //container.Register<ISerialize>(new JsonSerializer());
            //container.Register(BuildEventStore);

            return new Hera(builder);
        }

        #endregion
    }
}
