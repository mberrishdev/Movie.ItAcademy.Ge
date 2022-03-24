using Movie.Worker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.HostedServices
{
    public class WebDataRelodeService
    {
        private readonly int _updateTimeInSeconds = 3600; //1 hour
        private Timer _timer;

        public readonly IWebServices _webServices;
        public WebDataRelodeService(IWebServices webServices )
        {
            _webServices = webServices;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_updateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _webServices.RelodeWebData();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
