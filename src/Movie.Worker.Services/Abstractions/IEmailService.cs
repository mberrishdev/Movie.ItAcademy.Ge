using Movie.Persistance.Context;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface IEmailService
    {
        Task CheckAndSendEmail(MovieDBContext dBContext);
    }
}
