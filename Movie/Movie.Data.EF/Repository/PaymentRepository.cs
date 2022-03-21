using Movie.Domain.POCO;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IBaseRepository<Payment> _baseRepository;

        public PaymentRepository(IBaseRepository<Payment> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task CreatePaymentAsync(Payment paymentModel)
        {
            await _baseRepository.AddAsync(paymentModel);
        }
    }
}
