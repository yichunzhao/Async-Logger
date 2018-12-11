using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LogTest
{
    public class AsynTextLogger : ILogger
    {
        private readonly ILogEventDispatcher logEventDispatcher = null;

        public AsynTextLogger(ILogEventDispatcher logEventDispatcher)
        {
            this.logEventDispatcher = logEventDispatcher;
        }

        public void Start()
        {
            logEventDispatcher.Start();
        }

        public void StopWithFlush()
        {
            logEventDispatcher.Stop(true);
        }

        public void StopWithoutFlush()
        {
            logEventDispatcher.Stop(false);
        } 

        public void Log(LogEvent logEvent)
        {
            logEventDispatcher.Post(logEvent);
        }

        public void Write(string text)
        {
            throw new NotImplementedException();
        }

        public BlockingCollection<LogEvent> List()
        {
            return logEventDispatcher.List();
        }

    }
}
