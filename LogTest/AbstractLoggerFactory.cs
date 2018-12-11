using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogTest
{
    public abstract class LoggerFactory
    {
        public abstract ILogger getLogger();

        public ILogger Logger()
        {
            return getLogger();
        }
    }
}
