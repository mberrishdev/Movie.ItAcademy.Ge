using Microsoft.Extensions.Hosting;
using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.HostedServices
{
    public class EmailService : IHostedService, IDisposable
    {
        private int UpdateTimeInSeconds { get; set; }
        private Timer _timer;

        private readonly IEmailService _emailService;
        private readonly IServerOptionService _serverOptionService;

        public EmailService(IEmailService emailService, IServerOptionService serverOptionService)
        {
            _emailService = emailService;
            _serverOptionService = serverOptionService;
            GetUpdateTime();
        }

        public async void GetUpdateTime()
        {
            //await _serverOptionService.LoadServerOptions();
            var option = await  _serverOptionService.GetOptionAsync("move.worker.message.sender.time.sec");
            UpdateTimeInSeconds = int.Parse(option.Value);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(UpdateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _emailService.CheckAndSendEmail();
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