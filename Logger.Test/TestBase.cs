using System.IO;
using Logger.Data.EF;
using Logger.Service;
using Logger.Service.LogClasses;
using Logger.Service.LogInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Logger.Test
{
    public class TestBase
    {
        private string dbFileName = "log-test.db";
        private ServiceCollection serviceCollection;

        protected LogService _logService = null;
        protected Mock<IDbLogger> _dbLoggerMock = null;
        protected Mock<IFileLogger> _fileLoggerMock = null;

        protected DataContext dataContext { get; set; }

        protected ServiceProvider ServiceProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            InitializeServices();

            dataContext = ServiceProvider.GetService<DataContext>();
            dataContext.Database.EnsureCreated();
        }

        private void InitializeServices()
        {
            serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<DataContext>(options =>
                options.UseSqlite($"Data Source={dbFileName}"));
            
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete($"{dbFileName}");
        }

    }
}