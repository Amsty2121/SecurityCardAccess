using Application.Configurations;
using Application.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AuthOptions _authOptions;

        public AccountService(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions.Value;
        }

        public async Task<string> Login(string username, string password, CancellationToken cancellationToken = default)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (!checkingPasswordResult.Succeeded)
            {
                var errorMessage = "Login failed - Wrong credentials";
                throw new AccountLoginException(errorMessage);
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                var errorMessage = "Login failed - No such username in database";
                throw new AccountLoginException(errorMessage);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(nameof(User.Id), user.Id.ToString()),
                new Claim(nameof(User.UserName), user.UserName),
                new Claim(nameof(User.Department), user.Department)
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim("Roles", role));
                }

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

        public async Task Register(User user, string password, string role, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join('\n', result.Errors.Select(x => x.Description).ToArray());
                throw new AccountRegisterException(errorMessage);
            }

            await _userManager.AddToRoleAsync(user, role);

            //var userResult = await _userManager.FindByNameAsync(user.UserName);
        }
    }
}
