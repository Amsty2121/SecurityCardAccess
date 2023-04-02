using AdminApi.Models;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await _accountService.Login(loginRequest.Login, loginRequest.Password, cancellationToken);

                return Ok(token);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("registration")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken=default)
        {
            var user = _mapper.Map<User>(registerRequest);
            try
            {
                await _accountService.Register(user, registerRequest.Password, registerRequest.Role.ToString(), cancellationToken);
            }
            catch (AccountRegisterException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}