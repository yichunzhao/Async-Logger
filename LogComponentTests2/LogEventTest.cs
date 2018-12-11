using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogTest.Tests
{
    [TestClass]
    public class LogEventTest
    {
        [TestMethod]
        public void TestCreateMethod()
        {
            String msg = "error msg ...";
            LogEvent expected = LogEvent.Create(msg, DateTime.Now);
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(LogEvent));
            Assert.IsNotNull(expected.ToString());
            Assert.IsTrue(expected.ToString().Contains(msg));
        }

        [TestMethod()]
        public void WhenCurrentimeAcrossMidNight_ThenItReturnTrue()
        {
            DateTime currentTime = DateTime.Parse("10-10-2018 00:01");
            DateTime previousTime = DateTime.Parse("09-10-2018 23:59");

            Assert.IsTrue(LogEvent.IsAcrossMidNight(currentTime, previousTime));
        }

        [TestMethod()]
        public void WhenTimeAcrossMidNight_AndAtEndOfMonth_ItReturnTrue()
        {
            DateTime currentTime = DateTime.Parse("01-11-2018 00:01");
            DateTime previousTime = DateTime.Parse("31-10-2018 23:59");

            Assert.IsTrue(LogEvent.IsAcrossMidNight(currentTime, previousTime));
        }

        [TestMethod()]
        public void WhenTimeAcrossMidNight_AndAtEndOfYear_ItReturnTrue()
        {
            DateTime currentTime = DateTime.Parse("01-01-2019 00:01");
            DateTime previousTime = DateTime.Parse("31-12-2018 23:59");

            Assert.IsTrue(LogEvent.IsAcrossMidNight(currentTime, previousTime));
        }

        [TestMethod()]
        public void WhenCurrentimeNotAcrossMidNight_ThenItReturnFalse()
        {
            DateTime currentTime = DateTime.Parse("09-10-2018 23:59");
            DateTime previousTime = DateTime.Parse("09-10-2018 23:58");

            Assert.IsFalse(LogEvent.IsAcrossMidNight(currentTime, previousTime));
        }

    }

}
