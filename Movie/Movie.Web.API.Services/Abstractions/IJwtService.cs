using Movie.Services.Enums;
using Movie.Web.API.Services.Models;
using System;

namespace Movie.Web.API.Services.Abstractions
{
    public interface IJwtService
    {
        JwtToken GenerateSecurityToken(string userName, Guid userId, Roles role);
    }
}
