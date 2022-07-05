using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class AspNetUserRepository : IAspNetUserRepository
    {
        private readonly IBaseRepository<IdentityUser> _baseRepository;

        public AspNetUserRepository(IBaseRepository<IdentityUser> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            return await _baseRepository.GetAllAsync();
        }
    }
}
