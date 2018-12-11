namespace LogTest
{
    using System;
    using System.Text;

    public class LogEvent
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        private LogEvent(String text, DateTime timestamp)
        {
            this.Text = text;
            this.Timestamp = timestamp;
        }

        public static LogEvent Create(String text, DateTime timestamp)
        {
            return new LogEvent(text, timestamp);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            sb.Append("\t");
            sb.Append(Text);
            sb.Append("\t");
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        public static bool IsAcrossMidNight(DateTime current, DateTime previous)
        {
            bool yes = false;

            if (current > previous)
            {
                if (current.Year > previous.Year) yes = true;
                if (current.Month > previous.Month) yes = true;
                if (current.Day > previous.Day) yes = true;
            };

            return yes;
        }

    }
}