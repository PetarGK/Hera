using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Hera.Persistence;
using Hera.ExampleModel;
using Hera.DomainModeling.Repository;

namespace Hera.Tests
{
    [TestClass]
    public class AggregatePersistenceTests
    {
        private OrderId _orderId = OrderId.NewId;
        private CustomerId _customerId = new CustomerId(Guid.NewGuid());
        private IContainer _container;


        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();

            Hera.Init(builder)
                .SetupPersistence()
                .UsingInMemoryPersistence()
                .UsingBinarySerialization();

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

            using (var scope = _container.BeginLifetimeScope("unitOfWork"))
            {
                var aggregateRepository = scope.Resolve<IAggregateRepository>();
                var order = CreateOrder();

                // act
                aggregateRepository.Save(order);

                // assert

            }
        }

        [TestMethod]
        public void LoadAggregate()
        {

        }

        private Order CreateOrder()
        {
            return new Order(_customerId, _orderId);
        }
    }
}
