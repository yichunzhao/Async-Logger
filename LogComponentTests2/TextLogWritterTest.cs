using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogTest.Tests
{
    [TestClass]
    public class TextLogWritterTest
    {
        [TestMethod]
        public void WhenInvokingCreateMethod_ThenFileWritterCreated()
        {
            LogFileWritter writter = LogFileWritter.Create();
            Assert.IsNotNull(writter);

            writter.Log(LogEvent.Create("error ... ", DateTime.Now));

            Assert.IsTrue(File.Exists(writter.FilePathName()));
            FileInfo fileInfo = new FileInfo(writter.FilePathName());
            Assert.IsTrue(fileInfo.Length > 0);
            writter = null;
        }

        [TestMethod()]
        public void WhenInvokingLoggerWrite_ThenLogFileBecomeExisted()
        {
            LogFileWritter writter = LogFileWritter.Create();

            new Thread(() =>
            {
                writter.Log(LogEvent.Create("error: ", DateTime.Now));
                writter.Log(LogEvent.Create("warn : ", DateTime.Now));
                writter.Log(LogEvent.Create("info : ", DateTime.Now));
                writter.Log(LogEvent.Create("debug: ", DateTime.Now));
            }).Start();

            Assert.IsTrue(File.Exists(writter.FilePathName()));
            FileInfo fileInfo = new FileInfo(writter.FilePathName());
            long fileSize = fileInfo.Length;
            Assert.IsTrue(fileSize > 0);
            writter = null;
        }

    }
}
