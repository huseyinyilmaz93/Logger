using System;
using System.IO;
using System.Text.Json;
using Logger.Common.Extensions;
using Logger.Data.EF;
using Logger.Service.LogInterfaces;

namespace Logger.Service.LogClasses
{
    public class FileLogger : IFileLogger
    {
        public Log Add(string message, object obj)
        {
            DateTime now = DateTime.Now;
            Guid guid = Guid.NewGuid();
            Log log = new Log {Id = guid, Message = message, Logger = obj?.ToString(), CreatedDate = now };
            File.WriteAllText(log.GetLogPath(), JsonSerializer.Serialize(log));
            return log;
        }
    }

    public interface IFileLogger : ILogger
    {
        Log Add(string message, object obj);
    }
}
