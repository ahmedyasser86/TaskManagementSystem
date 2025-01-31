using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Helpers;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Application.Services
{
    public class JwtTokenGenerator(IOptions<JwtSettings> settings) : IJwtTokenGenerator
    {
        private readonly JwtSettings settings = settings.Value;

        public dynamic GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> userClaims)
        {
            var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            claims.AddRange(userClaims);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(settings.ExpiryDays),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            );

            return new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                token.ValidTo,
            };
        }
    }
}
