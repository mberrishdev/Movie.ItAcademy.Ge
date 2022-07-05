using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class LogService : ILogService
    {
        private IBaseRepository _repository;

        public async Task CheckAndArchive(MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            System.Collections.Generic.List<BOWebLog> boWebLogs = await _repository.GetAllAsync<BOWebLog>();
            System.Collections.Generic.List<MVCWebLog> mvcWebLogs = await _repository.GetAllAsync<MVCWebLog>();
            System.Collections.Generic.List<APIWebLog> apiWebLogs = await _repository.GetAllAsync<APIWebLog>();

            ServerOption option = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "movie.logs.archiver.time.sec");

            int logsArchiverTime = int.Parse(option.Value);

            foreach (BOWebLog boWebLog in boWebLogs)
            {
                if ((boWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await _repository.AddAsync<ArchiveLog>(new ArchiveLog()
                    {
                        Message = boWebLog.Message,
                        Level = boWebLog.Level,
                        Timestamp = boWebLog.Timestamp,
                        Exception = boWebLog.Exception,
                        LogEvent = boWebLog.LogEvent,
                    });
                    await _repository.RemoveAsync<BOWebLog>(boWebLog);
                }
            }

            foreach (MVCWebLog mvcWebLog in mvcWebLogs)
            {
                if ((mvcWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await _repository.AddAsync<ArchiveLog>(new ArchiveLog()
                    {
                        Message = mvcWebLog.Message,
                        Level = mvcWebLog.Level,
                        Timestamp = mvcWebLog.Timestamp,
                        Exception = mvcWebLog.Exception,
                        LogEvent = mvcWebLog.LogEvent,
                    });
                    await _repository.RemoveAsync<MVCWebLog>(mvcWebLog);
                }
            }

            foreach (APIWebLog apiWebLog in apiWebLogs)
            {
                if ((apiWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await _repository.AddAsync<ArchiveLog>(new ArchiveLog()
                    {
                        Message = apiWebLog.Message,
                        Level = apiWebLog.Level,
                        Timestamp = apiWebLog.Timestamp,
                        Exception = apiWebLog.Exception,
                        LogEvent = apiWebLog.LogEvent,
                    });
                    await _repository.RemoveAsync<APIWebLog>(apiWebLog);
                }
            }
        }
    }
}
