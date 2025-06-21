
namespace SimpleLogger.Services
{
    public class FileLoggerService:ILogger,IDisposable
    {
        string path = @"..\SimpleLogger\Data\data.txt";

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }
        public void Dispose() {}
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(formatter(state, exception));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("logging error - " + ex.Message);
            }
            
        }
    }
}
