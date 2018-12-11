using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogTest
{
    public class LogFileWritter : ILogEventListener
    {
        private static readonly String root = @"C:\LogTest";
        private static readonly String logFileName = root + @"\Log" + DateTime.Now.ToString("yyyyMMdd") + ".log";

        private static readonly String logTitle = "Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine;

        private StreamWriter writer;
        private DateTime previous = DateTime.Now;

        private LogFileWritter()
        {
        }

        public static LogFileWritter Create()
        {
            return new LogFileWritter();
        }

        public void Log(LogEvent logEvent)
        {
            var current = logEvent.Timestamp;

            if (writer == null) Init();

            InitNewLogInNewDay(current, previous);

            writer.Write(logEvent.ToString());

            previous = current;
        }

        private void Init()
        {
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            writer = File.AppendText(logFileName);
            writer.Write(logTitle);
            writer.AutoFlush = true;
        }

        private void InitNewLogInNewDay(DateTime current, DateTime previous)
        {
            if (LogEvent.IsAcrossMidNight(current, previous)) Init();
        }


        public String FilePathName()
        {
            return logFileName;
        }

    }
}
