using Microsoft.Extensions.Hosting;
using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.HostedServices
{
    public class MessageSenderService : IHostedService, IDisposable
    {
        private int UpdateTimeInSeconds { get; set; }
        private Timer _timer;

        private readonly IMessageSenderService _messageSenderService;
        private readonly IServerOptionService _serverOptionService;

        public MessageSenderService(IMessageSenderService messageSenderService, IServerOptionService serverOptionService)
        { 
            _messageSenderService = messageSenderService;
            _serverOptionService = serverOptionService;
            GetUpdateTime();
        }

        public void GetUpdateTime()
        {
            //await _serverOptionService.LoadServerOptions();
            var option = _serverOptionService.GetOption("move.worker.message.sender.time.sec");
            UpdateTimeInSeconds = int.Parse(option.Value);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(UpdateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _messageSenderService.CheckAndSend();
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
