using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogTest
{
    public interface ILogEventDispatcher
    {
        void Start(); //start dispatcher dispatching 
        void Stop(bool withFlush);  //stop dispatcher dispatching
        ILogEventDispatcher AddEventListener(ILogEventListener logEventListener); //register listerners
        void Post(LogEvent logEvent);
        BlockingCollection<LogEvent> List(); //return logevents in the queue. 
    }
}
