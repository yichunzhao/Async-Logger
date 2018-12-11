using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LogTest
{
    public class LogEventQueueDispather : ILogEventDispatcher
    {
        private BlockingCollection<LogEvent> logEventQueue;
        private List<ILogEventListener> listeners;

        private Thread logWorker;

        private CancellationTokenSource takCancellationTokenSource;
        private CancellationToken takeToken;

        private CancellationTokenSource postCancellationTokenSource;
        private CancellationToken postToken;

        private LogEventQueueDispather()
        {
            logEventQueue = new BlockingCollection<LogEvent>(36);
            listeners = new List<ILogEventListener>();

        }

        private void Init()
        {
            takCancellationTokenSource = new CancellationTokenSource();
            takeToken = takCancellationTokenSource.Token;

            postCancellationTokenSource = new CancellationTokenSource();
            postToken = postCancellationTokenSource.Token;
        }

        public static LogEventQueueDispather Create()
        {
            return new LogEventQueueDispather();
        }

        public ILogEventDispatcher AddEventListener(ILogEventListener logEventListener)
        {
            listeners.Add(logEventListener);
            return this;
        }

        public BlockingCollection<LogEvent> List()
        {
            return logEventQueue;
        }

        public void Post(LogEvent logEvent)
        {
            if (postToken.IsCancellationRequested) return;
            logEventQueue.TryAdd(logEvent, Timeout.Infinite, postToken);

            Thread.Sleep(20);
        }

        public void Start()
        {
            Init();
            logWorker = new Thread(Dispatch);
            logWorker.Start();

            Thread.Sleep(10);
        }

        public void Stop(bool withFlush)
        {
            if (withFlush == false) takCancellationTokenSource.Cancel();//without flush
            if (withFlush == true) postCancellationTokenSource.Cancel();

        }

        private void Dispatch()
        {
           
            while (!takCancellationTokenSource.IsCancellationRequested)
            {

                try
                {
                    if (logEventQueue.TryTake(out LogEvent logEvent, Timeout.Infinite, takeToken))
                    {
                        listeners.ForEach(listener => listener.Log(logEvent));
                    };
                } catch{  }

            }
        }

    }


}
