using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConfigurationTest
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(_configuration.GetValue<string>("TestString"));
                _logger.LogInformation(_configuration.GetValue<string>("TestString2"));
                _logger.LogInformation(_configuration.GetValue<string>("TestString3"));
                _logger.LogInformation(Directory.GetCurrentDirectory());
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
