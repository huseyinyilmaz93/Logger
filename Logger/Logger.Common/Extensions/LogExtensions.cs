using Logger.Data.EF;

namespace Logger.Common.Extensions
{
    public static class LogExtensions
    {
        public static string GetLogPath(this Log log)
        {
            return string.Concat("Logs/", log.CreatedDate.ToLongDateString(), "-", log.Id.ToString());
        }
    }
}
