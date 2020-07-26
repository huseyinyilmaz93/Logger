using Logger.Data.EF;
using Logger.Service.LogInterfaces;

namespace Logger.Service.LogClasses
{
    public class DbLogger : IDbLogger
    {
        private readonly DataContext _dataContext;
        
        public DbLogger(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Log Add(string message, object obj)
        {
            Log log = new Log()
            {
                Message = message,
                Logger = obj?.GetType().ToString()
            };

            _dataContext.Logs.Add(log);
            _dataContext.SaveChanges();
            return log;
        }
    }

    public interface IDbLogger : ILogger
    {
        Log Add(string message, object obj);
    }

}
