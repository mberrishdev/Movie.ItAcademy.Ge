using Microsoft.Extensions.Hosting;
using Movie.Worker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.HostedServices
{
    public class RoomArchiverService : IHostedService, IDisposable
    {
        private readonly int _updateTimeInSeconds = 60;
        private Timer _timer;

        public readonly IRoomService _roomService;
        public RoomArchiverService(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_updateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
             _roomService.CheckAndArchiveRoom();
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
