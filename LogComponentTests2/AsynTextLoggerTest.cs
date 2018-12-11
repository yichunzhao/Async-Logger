using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogTest.Tests
{
    [TestClass]
    public class AsyncTextLoggerTest
    {
        [TestMethod]
        public void WhenAddOneLogEvent_DispatcherStop_ThenEventMeasuredInQueue()
        {
            AsynTextLogger textLogger = new AsynTextLogger(LogEventQueueDispather.Create());
            textLogger.Log(LogEvent.Create("error ...", DateTime.Now));
            textLogger.Log(LogEvent.Create("error ...", DateTime.Now));
            BlockingCollection<LogEvent> bc = textLogger.List();
            Assert.AreEqual(2, bc.Count);
            textLogger = null;
        }

        [TestMethod]
        public void WhenPost15EventsInThreads_WhileDispatcherStop_ThenCount15InQueue()
        {
            AsynTextLogger textLogger = new AsynTextLogger(LogEventQueueDispather.Create());

            for (int i = 0; i < 15; i++)
            {
                new Thread(() =>
                {
                    textLogger.Log(LogEvent.Create("error ..." + i, DateTime.Now));
                }).Start();

                Thread.Sleep(5);
            }

            BlockingCollection<LogEvent> bc = textLogger.List();
            Assert.AreEqual(15, bc.Count);
            textLogger = null;
        }

        [TestMethod]
        public void WhenPost15Event_WhileStartDispatch_ThenQueueCount0()
        {
            AsynTextLogger textLogger = new AsynTextLogger(LogEventQueueDispather.Create());
            textLogger.Start();
            Thread.Sleep(20);

            for (int i = 0; i < 15; i++)
            {
                textLogger.Log(LogEvent.Create("error ..." + i, DateTime.Now));
            };


        BlockingCollection<LogEvent> bc = textLogger.List();
        Assert.AreEqual(0, bc.Count);
            textLogger = null;
        }

    [TestMethod]
    public void WhenAddOneLogEvent_StartLoggerWorker_QueueCountZero()
    {
        AsynTextLogger textLogger = new AsynTextLogger(LogEventQueueDispather.Create());
        textLogger.Start();
        textLogger.Log(LogEvent.Create("error ...", DateTime.Now));
        BlockingCollection<LogEvent> bc = textLogger.List();
        Assert.AreEqual(0, bc.Count);
        textLogger = null;
    }

    [TestMethod]
    public void WhenAddManyLogEvent_StartLoggerWorker_HavingListener_QueueCountZero()
    {
        AsynTextLogger textLogger = new AsynTextLogger(
            LogEventQueueDispather.Create().AddEventListener(LogFileWritter.Create())
            );
        textLogger.Start();

        Thread.Sleep(50);

        new Thread(() =>
        {
            for (int i = 0; i < 15; i++)
            {
                textLogger.Log(LogEvent.Create("error ..." + i, DateTime.Now));
            }

        }).Start();

        Assert.AreEqual(0, textLogger.List().Count);
        textLogger = null;
    }

    [TestMethod()]
    public void StopWithoutFlushTest()
    {

        AsynTextLogger textLogger = new AsynTextLogger(
            LogEventQueueDispather.Create().AddEventListener(LogFileWritter.Create())
            );

        textLogger.Start(); //start logger to log
        Thread.Sleep(200);         //simulate to do something in current thread.   
        textLogger.StopWithoutFlush(); //stop logging without flush. 

        //meanwhile other threads post log into the dispacher
        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th1: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th2: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th3: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th4: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th5: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th6: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        //expect some outstanding logevents in the q. 
        Assert.IsTrue(textLogger.List().Count > 0);
        textLogger = null;
    }

    [TestMethod()]
    public void StopWithFlushTest()
    {

        AsynTextLogger textLogger = new AsynTextLogger(
            LogEventQueueDispather.Create().AddEventListener(LogFileWritter.Create())
            );

        textLogger.Start(); //start logger to log
        Thread.Sleep(200);         //simulate to do something in current thread.   
        textLogger.StopWithFlush(); //stop logging without flush. 

        //meanwhile other threads post log into the dispacher
        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th1: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th2: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th3: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th4: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th5: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        new Thread(() =>
        {
            for (int i = 0; i < 1350; i++)
            {
                textLogger.Log(LogEvent.Create("Test Th6: Number without Flush: " + i.ToString(), DateTime.Now));
            };

        }).Start();

        //expect having no outstanding logevents in the q. 
        Assert.IsTrue(textLogger.List().Count == 0);
        textLogger = null;
    }

}
}
