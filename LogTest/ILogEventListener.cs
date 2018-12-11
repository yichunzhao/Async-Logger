using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogTest
{
    public interface ILogEventListener
    {
        void Log(LogEvent e);
    }
}
