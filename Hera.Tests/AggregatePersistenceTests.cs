using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Hera.Persistence;
using Hera.ExampleModel;

namespace Hera.Tests
{
    [TestClass]
    public class AggregatePersistenceTests
    {
        private OrderId _orderId = OrderId.NewId;
        private string _bucket = "Default";
        private IContainer _container;


        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();

            Hera.Init(builder)
                .SetupPersistence()
                .UsingInMemoryPersistence();

            _container = builder.Build();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container.Dispose();
        }

        [TestMethod]
        public void SaveAggregate()
        {
            // arrange
            var aggregateRepository = _container.Resolve<IAggregateRepository>();
            var order = CreateOrder();

            // act
            aggregateRepository.Save(order, _bucket);

            // assert


        }

        [TestMethod]
        public void LoadAggregate()
        {

        }

        private Order CreateOrder()
        {
            return new Order(_orderId);
        }
    }
}
