using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Movie.Data
{
    public interface IAspNetUserRepository
    {
        Task<List<IdentityUser>> GetUsersAsync();
    }
}
