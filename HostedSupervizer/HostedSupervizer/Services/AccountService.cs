using HostedSupervizer.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

namespace HostedSupervizer.Services
{
    public class AccountService
    {
        private readonly AuthOptions _authOptions;

        public AccountService(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        public async Task<string> GetToken(CancellationToken cancellationToken = default)
        {
            var id = Guid.NewGuid();
            var role = "Supervizer";
            var claims = new List<Claim>()
            {
                new Claim("Id", id.ToString()),
                new Claim("Roles", role)
            };

            var signinCredentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(),
                                                           SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_authOptions.TokenLifetime),
                signingCredentials: signinCredentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

            return encodedToken;
        }
    }
}
