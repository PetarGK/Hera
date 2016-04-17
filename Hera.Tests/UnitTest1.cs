using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Hera.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new ContainerBuilder();

            Hera.Init(builder)
                .SetupSnapshotting();
        }
    }
}
