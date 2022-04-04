using Movie.Domain.POCO;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IPaymentRepository
    {
        Task CreatePaymentAsync(Payment paymentModel);
    }
}
