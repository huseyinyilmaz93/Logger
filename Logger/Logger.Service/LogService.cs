using System.Linq;
using Logger.Data.EF;
using Logger.Service.LogClasses;

namespace Logger.Service
{
    public class LogService : ILogService
    {
        private readonly IDbLogger _dbLogger;

        private readonly IFileLogger _fileLogger;

        private readonly DataContext _dataContext;

        public LogService(IDbLogger dbLogger, IFileLogger fileLogger, DataContext dataContext)
        {
            _dbLogger = dbLogger;
            _fileLogger = fileLogger;
            _dataContext = dataContext;
        }

        public Log AddLog(string message, object obj)
        {
            try
            {
                return _dbLogger.Add(message, obj);
            }
            catch
            {
                return _fileLogger.Add(message, obj);
            }
        }

        public Log[] GetLogs()
        {
            return _dataContext.Logs.ToArray();
        }
    }

    public interface ILogService
    {
        Log AddLog(string message, object obj);
        Log[] GetLogs();
    }
}
