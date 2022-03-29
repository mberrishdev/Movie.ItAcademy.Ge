//using Microsoft.Extensions.Hosting;
//using Movie.Services.Abstractions;
//using Movie.Worker.Services.Abstractions;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Movie.Worker.Services.HostedServices
//{

//    public class BookingCancellerService : IHostedService, IDisposable
//    {
//        private int UpdateTimeInSeconds { get; set; }
//        private Timer _timer;

//        private readonly IBookingService _bookingService;
//        private readonly IServerOptionService _serverOptionService;

//        public BookingCancellerService(IBookingService bookingService, IServerOptionService serverOptionService)
//        {
//            _bookingService = bookingService;
//            _serverOptionService = serverOptionService;
//            GetUpdateTime();
//        }

//        public async void GetUpdateTime()
//        {
//            var option = await _serverOptionService.GetOptionAsync("move.worker.booking.canceller.int.time.sec");
//            UpdateTimeInSeconds = int.Parse(option.Value);
//        }

//        public Task StartAsync(CancellationToken stoppingToken)
//        {
//            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(UpdateTimeInSeconds));
//            return Task.CompletedTask;
//        }

//        private void DoWork(object state)
//        {
//            _bookingService.CheckAndCancellBookings();
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
