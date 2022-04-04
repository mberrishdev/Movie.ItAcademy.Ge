using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Persistance.Context;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Services.BackgroudWorkers
{
    public class WebDataRelodeWorker : BackgroundService
    {
        private int UpdateTimeInSeconds { get; set; }
        private readonly IServiceProvider _serviceProvider;

        public WebDataRelodeWorker(IServiceProvider serviceProvider)
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
                .GetOptionAsync("move.worker.web.data.relode.int.time.sec", dbContext);

            UpdateTimeInSeconds = int.Parse(option.Value);

            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IWebServices service = scope.ServiceProvider.GetRequiredService<IWebServices>();

                    await service.RelodeWebData(dbContext);
                }

                await Task.Delay(UpdateTimeInSeconds, stoppingToken);
            }
        }
    }
}
