using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public interface IArchiveLogRepository
    {
        Task AddArchiveLogAsync(ArchiveLog model);
    }
}
