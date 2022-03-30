﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.BackgroudWorkers
{
    public class RoomCheckerWorker : BackgroundService
    {
        private int UpdateTimeInSeconds { get; set; }

        private readonly Movie.Services.Abstractions.IServerOptionService _serverOptionService;
        private readonly IServiceProvider _serviceProvider;

        public RoomCheckerWorker(Movie.Services.Abstractions.IServerOptionService serverOptionService, IServiceProvider serviceProvider)
        {
            _serverOptionService = serverOptionService;
            _serviceProvider = serviceProvider;
        }

        public async Task GetUpdateTime()
        {
            var option = await _serverOptionService.GetOptionAsync("move.worker.room.checker.int.time.sec");
            UpdateTimeInSeconds = int.Parse(option.Value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await GetUpdateTime();
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IRoomService>();

                    await service.CheckIfRoomHasMovie();
                }

                await Task.Delay(UpdateTimeInSeconds, stoppingToken);
            }
        }
    }
}