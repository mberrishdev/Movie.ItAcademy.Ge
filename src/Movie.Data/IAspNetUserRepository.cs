using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IAspNetUserRepository
    {
        Task<List<IdentityUser>> GetUsersAsync();
    }
}
