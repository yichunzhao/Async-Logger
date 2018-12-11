using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogTest
{
    public class AsyncTextLoggerFactory : LoggerFactory
    {
        public override ILogger getLogger()
        {
            return new AsynTextLogger(LogEventQueueDispather.Create().AddEventListener(LogFileWritter.Create()));
        }
    }
}
