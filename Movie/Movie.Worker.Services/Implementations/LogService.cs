using Microsoft.EntityFrameworkCore;
using Movie.Data.LogRepository;
using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class LogService : ILogService
    {
        public async Task CheckAndArchive(MovieDBContext dBContext)
        {
            var boWebLogs = await dBContext.BOWebLogs.ToListAsync();
            var mvcWebLogs = await dBContext.MVCWebLogs.ToListAsync();
            var apiWebLogs = await dBContext.APIWebLogs.ToListAsync();

            var option = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "movie.logs.archiver.time.sec");
            int logsArchiverTime = int.Parse(option.Value);

            foreach (var boWebLog in boWebLogs)
            {
                if ((boWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await dBContext.ArchiveLogs.AddAsync(new ArchiveLog()
                    {
                        Message = boWebLog.Message,
                        Level = boWebLog.Level,
                        Timestamp = boWebLog.Timestamp,
                        Exception = boWebLog.Exception,
                        LogEvent = boWebLog.LogEvent,
                    });
                    dBContext.BOWebLogs.Remove(boWebLog);
                    await dBContext.SaveChangesAsync();
                }
            }

            foreach (var mvcWebLog in mvcWebLogs)
            {
                if ((mvcWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await dBContext.ArchiveLogs.AddAsync(new ArchiveLog()
                    {
                        Message = mvcWebLog.Message,
                        Level = mvcWebLog.Level,
                        Timestamp = mvcWebLog.Timestamp,
                        Exception = mvcWebLog.Exception,
                        LogEvent = mvcWebLog.LogEvent,
                    });
                    dBContext.MVCWebLogs.Remove(mvcWebLog);
                    await dBContext.SaveChangesAsync();
                }
            }

            foreach (var apiWebLog in apiWebLogs)
            {
                if ((apiWebLog.Timestamp - DateTime.UtcNow).TotalSeconds <= logsArchiverTime)
                {
                    await dBContext.ArchiveLogs.AddAsync(new ArchiveLog()
                    {
                        Message = apiWebLog.Message,
                        Level = apiWebLog.Level,
                        Timestamp = apiWebLog.Timestamp,
                        Exception = apiWebLog.Exception,
                        LogEvent = apiWebLog.LogEvent,
                    });
                    dBContext.APIWebLogs.Remove(apiWebLog);
                    await dBContext.SaveChangesAsync();
                }
            }
        }
    }
}
