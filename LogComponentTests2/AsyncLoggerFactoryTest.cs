using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogTest.Tests
{
    [TestClass]
    public class AsyncLoggerFactoryTest
    {
        [TestMethod]
        public void WhenCreateAsynLoggerFactory_ThenReturnAsycTextLogger()
        {
            LoggerFactory loggerFactory = new AsyncTextLoggerFactory();
            ILogger logger = loggerFactory.getLogger();
            Assert.IsNotNull(logger);
            Assert.IsInstanceOfType(logger, typeof(AsynTextLogger));
          
        }
    }
}
