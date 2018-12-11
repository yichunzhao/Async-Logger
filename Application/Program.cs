using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUsers
{
    using System.Threading;

    using LogTest;

    class Program
    {
        static void Main(string[] args)
        {
            LoggerFactory loggerFactory = new AsyncTextLoggerFactory();

            ILogger logger = loggerFactory.getLogger();
            logger.Start();

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("logger 1 write: ");
                logger.Log(LogEvent.Create("Number with Flush: " + i.ToString(), DateTime.Now));
                Thread.Sleep(50);
            }

            logger.StopWithFlush();
            logger.Start();


            ILogger logger2 = loggerFactory.getLogger();
            logger2.Start();


            for (int i = 50; i > 0; i--)
            {
                Console.WriteLine("logger 2 write: ");
                logger.Log(LogEvent.Create("Number with No flush: " + i.ToString(), DateTime.Now));
                Thread.Sleep(20);
            }

            logger2.StopWithoutFlush();
            logger2.Start();

            Console.WriteLine("end of log ... ");
            Console.ReadLine();

        }
    }
}
