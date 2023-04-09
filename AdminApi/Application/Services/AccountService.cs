using Application.Configurations;
using Application.IServices;
using Domain.Entities;
using Domain.Exceptions;
using IdentityServer4.Extensions;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

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

        public async Task<object?> Register(User user, string password, string role, CancellationToken cancellationToken = default)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var creationResult = await _userManager.CreateAsync(user, password);

            var roleAddingResult = await _userManager.AddToRoleAsync(user, role);

            

            if (!creationResult.Succeeded)
            {
                var errorMessage = string.Join('\n', creationResult.Errors.Select(x => x.Description).ToArray());
                throw new AccountRegisterException(errorMessage);
            }

            if (!roleAddingResult.Succeeded)
            {
                var errorMessage = string.Join('\n', roleAddingResult.Errors.Select(x => x.Description).ToArray());
                throw new AccountRegisterException(errorMessage);
            }
            var resultUser = _userManager.FindByNameAsync(user.UserName).Result;

			scope.Complete();
			return creationResult.Succeeded && roleAddingResult.Succeeded ? 
                new { Id = resultUser.Id, Username = resultUser.UserName, UserRole = role, Department = user.Department }
                 : null;
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            
            if (user == null)
            {
                return false;
            }

            return _userManager.DeleteAsync(user).Result.Succeeded;
        }

        public async Task<ICollection<object>> GetAllUsersByRole(string role, CancellationToken cancellation = default)
        {
            return !string.IsNullOrWhiteSpace(role) ? _userManager.GetUsersInRoleAsync(role)
                .Result.Select(x => new {   Id = x.Id,
				                            Username = x.UserName,
				                            UserRole = role,
				                            Department = x.Department
			                            }).ToList<object>()

                : _userManager.Users.ToList().Select(x => new { Id = x.Id,
					                                            Username = x.UserName,
					                                            UserRole = _userManager.GetRolesAsync(x).Result.FirstOrDefault(),
					                                            Department = x.Department
				}).ToList<object>();



			return _userManager.GetUsersInRoleAsync(role.ToString()).Result.Select(x => new {
                                                                                    Username = x.UserName,
                                                                                    UserRole = role.ToString(),
                                                                                    Department = x.Department
                                                                                            }).ToList<object>();

        }
    }
}
