using System;
using System.IO;
using Logger.Common.Extensions;
using Logger.Data.EF;
using Logger.Service;
using Logger.Service.LogClasses;
using Logger.Service.LogInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Logger.Test
{
    [TestFixture]
    public class LogTest : TestBase
    {
        [Test]
        public void AddLog__log_db_save_method_returns_mocked_data()
        {
            _dbLoggerMock = new Mock<IDbLogger>();
            _fileLoggerMock = new Mock<IFileLogger>();
            _logService = new LogService(_dbLoggerMock.Object, _fileLoggerMock.Object, dataContext);

            var returnLog = new Log() { Logger = typeof(LogTest).ToString(), Message = "q1"};

            _dbLoggerMock.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(returnLog);
            
            Log dbRecord = _logService.AddLog("Test", typeof(LogTest));

            Assert.AreEqual(returnLog, dbRecord);
        }

        [Test]
        public void AddLog__log_file_save_method_returns_mocked_data()
        {
            _dbLoggerMock = new Mock<IDbLogger>();
            _fileLoggerMock = new Mock<IFileLogger>();
            _logService = new LogService(_dbLoggerMock.Object, _fileLoggerMock.Object, dataContext);

            var mockedLog = new Log() { Logger = typeof(LogTest).ToString(), Message = "q2" };

            _dbLoggerMock.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<object>()))
                .Throws<SystemException>();
            _fileLoggerMock.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(mockedLog);

            Log log = _logService.AddLog("Test", typeof(LogTest));

            Assert.AreEqual(mockedLog, log);
        }
    }

    [TestFixture]
    public class FileLoggerTest : TestBase
    {
        [Test]
        public void Add__log_saves_to_file()
        {
            var fileLogger = new FileLogger();

            Log log = fileLogger.Add("test", typeof(LogTest));

            bool logExists = File.Exists(log.GetLogPath());
            Assert.AreEqual(logExists, true);
        }
    }

    [TestFixture]
    public class DbLoggerTest : TestBase
    {
        [Test]
        public void Add__log_saves_to_database()
        {
            _dbLoggerMock = new Mock<IDbLogger>();
            _fileLoggerMock = new Mock<IFileLogger>();
            _logService = new LogService(_dbLoggerMock.Object, _fileLoggerMock.Object, dataContext);

            var dbLogger = new DbLogger(dataContext);
            dbLogger.Add("test", typeof(LogTest));

            Assert.Greater(_logService.GetLogs().Length, 0);
        }
    }
}
