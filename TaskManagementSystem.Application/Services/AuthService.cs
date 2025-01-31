using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Helpers;

namespace TaskManagementSystem.Application.Services
{
    public class AuthService(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtTokenGenerator, SignInManager<IdentityUser> signInManager) : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator = jwtTokenGenerator;

        public async Task<ResponseBase> LoginAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null || !await userManager.CheckPasswordAsync(user, password))
                return new ResponseBase(false, new { Email = email, Password = password }, "Invalid email or password"); // لم يتم العثور على المستخدم

            // جلب الأدوار والـ Claims الخاصة بالمستخدم
            var roles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);

            // إنشاء التوكن
            return new ResponseBase(true, new
            {
                Email = email,
                Roles = roles,
                Token = jwtTokenGenerator.GenerateToken(user, roles, userClaims)
            }
            );
        }

        public async Task<ResponseBase> RegisterAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var res = await userManager.CreateAsync(user, password);
            if (!res.Succeeded)
            {
                return new ResponseBase(false, new { Email = email, Password = password }, string.Join('\n', res.Errors.Select(m => m.Description).ToList()));
            }
            else
            {
                return new ResponseBase(true, new { Email = email, Password = password });
            }
        }
    }
}
