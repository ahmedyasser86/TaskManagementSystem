using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        dynamic GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> userClaims);
    }
}
