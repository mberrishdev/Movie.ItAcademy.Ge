using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Persistance.Context;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.BackgroudWorkers
{

    public class BookingCancellerWorker : BackgroundService
    {
        private int UpdateTimeInSeconds { get; set; }
        private readonly IServiceProvider _serviceProvider;

        public BookingCancellerWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope mainScope = _serviceProvider.CreateScope();
            MovieDBContext dbContext = mainScope.ServiceProvider.GetRequiredService<MovieDBContext>();

            IServerOptionService serverOptionSerice = mainScope.ServiceProvider
                .GetRequiredService<IServerOptionService>();

            Movie.Services.Models.ServerOption option = await serverOptionSerice
                .GetOptionAsync("move.worker.booking.canceller.int.time.sec", dbContext);

            UpdateTimeInSeconds = int.Parse(option.Value);

            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IBookingService service = scope.ServiceProvider.GetRequiredService<IBookingService>();

                    await service.CheckAndCancellBookings(dbContext);
                }

                await Task.Delay(UpdateTimeInSeconds, stoppingToken);
            }
        }
    }
}
