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
    public class RoomCheckerService : IHostedService, IDisposable
    {
        private int UpdateTimeInSeconds { get; set; }
        private Timer _timer;

        public readonly IRoomService _roomService;
        private readonly IServerOptionService _serverOptionService;

        public RoomCheckerService(IRoomService roomService, IServerOptionService serverOptionService)
        {
            _roomService = roomService;
            _serverOptionService = serverOptionService;
        }

        public void GetUpdateTime()
        {
            var option = _serverOptionService.GetOption("move.worker.room.checker.int.time.sec");
            UpdateTimeInSeconds = int.Parse(option.Value);
        }


        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(UpdateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
             _roomService.CheckIfRoomHasMovie();
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
