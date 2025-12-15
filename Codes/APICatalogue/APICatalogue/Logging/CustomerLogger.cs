namespace Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string name;
        readonly CustomerLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomerLoggerProviderConfiguration config)
        {
            this.name = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            WriteLog(message);
        }

        private void WriteLog(string message)
        {
            string path = @"C:\LogTest\CustomerLogs.txt";

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                try
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;

                }
            }
        }
    }
}
