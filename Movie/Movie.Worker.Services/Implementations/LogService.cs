using Movie.Data.LogRepository;
using Movie.Domain.POCO;
using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class LogService : ILogService
    {
        private readonly IBOWebLogRepository _boWebLogRepository;
        private readonly IMVCWebLogRepository _mvcWebLogRepository;
        private readonly IAPIWebLogRepository _apiWebLogRepository;
        private readonly IServerOptionService _serverOptionService;
        private readonly IArchiveLogRepository _archiveLogRepository;

        public LogService(IBOWebLogRepository boWebLogRepository,
            IMVCWebLogRepository mvcWebLogRepository,
            IAPIWebLogRepository apiWebLogRepository,
            IArchiveLogRepository archiveLogRepository,
            IServerOptionService serverOptionService)
        {
            _boWebLogRepository = boWebLogRepository;
            _mvcWebLogRepository = mvcWebLogRepository;
            _apiWebLogRepository = apiWebLogRepository;
            _serverOptionService = serverOptionService;
            _archiveLogRepository = archiveLogRepository;
        }

        public async Task CheckAndArchive()
        {
            var boWebLogs = await _boWebLogRepository.GetAllAsync();
            var mvcWebLogs = await _mvcWebLogRepository.GetAllAsync();
            var apiWebLogs = await _apiWebLogRepository.GetAllAsync();

            int logsArchiverTime = int.Parse(_serverOptionService.GetOption("movie.logs.archiver.time.sec").Value);
            foreach (var boWebLog in boWebLogs)
            {
                if ((boWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await _archiveLogRepository.AddArchiveLogAsync(new ArchiveLog()
                    {
                        Message = boWebLog.Message,
                        Level = boWebLog.Level,
                        Timestamp = boWebLog.Timestamp,
                        Exception = boWebLog.Exception,
                        LogEvent = boWebLog.LogEvent,
                    });
                    await _boWebLogRepository.DeleteAsync(boWebLog.Id);
                }
            }

            foreach (var mvcWebLog in mvcWebLogs)
            {
                if ((mvcWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await _archiveLogRepository.AddArchiveLogAsync(new ArchiveLog()
                    {
                        Message = mvcWebLog.Message,
                        Level = mvcWebLog.Level,
                        Timestamp = mvcWebLog.Timestamp,
                        Exception = mvcWebLog.Exception,
                        LogEvent = mvcWebLog.LogEvent,
                    });
                    await _mvcWebLogRepository.DeleteAsync(mvcWebLog.Id);
                }
            }

            foreach (var apiWebLog in apiWebLogs)
            {
                if ((apiWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await _archiveLogRepository.AddArchiveLogAsync(new ArchiveLog()
                    {
                        Message = apiWebLog.Message,
                        Level = apiWebLog.Level,
                        Timestamp = apiWebLog.Timestamp,
                        Exception = apiWebLog.Exception,
                        LogEvent = apiWebLog.LogEvent,
                    });
                    await _apiWebLogRepository.DeleteAsync(apiWebLog.Id);
                }
            }
        }
    }
}
