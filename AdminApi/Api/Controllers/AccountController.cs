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

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromQuery] RegisterRequest registerRequest, CancellationToken cancellationToken=default)
        {
            var user = _mapper.Map<User>(registerRequest);
            try
            {
                return await _accountService.Register(user, registerRequest.Password, registerRequest.Role.ToString(), cancellationToken) ? Ok() : BadRequest();
            }
            catch (AccountRegisterException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _accountService.Delete(id, cancellationToken) ? Ok() : BadRequest();
            }
            catch (AccountRegisterException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] RoleValue role, CancellationToken cancellationToken = default)
        {
            try
            {
                
                return Ok(await _accountService.GetAllUsersByRole(role, cancellationToken));
            }
            catch (AccountRegisterException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}