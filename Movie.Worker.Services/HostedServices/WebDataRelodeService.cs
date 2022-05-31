//using Microsoft.Extensions.Hosting;
//using Movie.Services.Abstractions;
//using Movie.Worker.Services.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Movie.Worker.Services.HostedServices
//{
//    public class WebDataRelodeService : IHostedService, IDisposable
//    {
//        private int UpdateTimeInSeconds { get; set; }
//        private Timer _timer;

//        public readonly IWebServices _webServices;
//        private readonly IServerOptionService _serverOptionService;

//        public WebDataRelodeService(IWebServices webServices, IServerOptionService serverOptionService)
//        {
//            _webServices = webServices;
//            _serverOptionService = serverOptionService;
//            GetUpdateTime();
//        }

//        public async void GetUpdateTime()
//        {
//            //await _serverOptionService.LoadServerOptions();
//            var option = await _serverOptionService.GetOptionAsync("move.worker.web.data.relode.int.time.sec");
//            UpdateTimeInSeconds = int.Parse(option.Value);
//        }


//        public Task StartAsync(CancellationToken stoppingToken)
//        {
//            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(UpdateTimeInSeconds));
//            return Task.CompletedTask;
//        }

//        private void DoWork(object state)
//        {
//            _webServices.RelodeWebData();
//        }

//        public Task StopAsync(CancellationToken stoppingToken)
//        {
//            _timer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _timer?.Dispose();
//        }
//    }
//}
