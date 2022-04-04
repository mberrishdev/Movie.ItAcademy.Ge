using Movie.Persistance.Context;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface IMessageSenderService
    {
        Task CheckAndSend(MovieDBContext dBContext);
    }
}
