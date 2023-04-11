using AdminApi.Extensions;
using AdminApi.Models;
using Application;
using Application.IServices;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessCardController : ControllerBase
    {
        private readonly ICardAccessService _cardAccessService;
        private readonly IMapper _mapper;

        public AccessCardController(ICardAccessService cardAccessService, IMapper mapper)
        {
            _cardAccessService = cardAccessService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAccessCard([FromBody] GenerateAccessCardRequest request, CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.Add(_mapper.Map<AccessCard>(request), token)).ToOk(c => true);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccessCard([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.Remove(id, token)).ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPatch("change-access")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ModifyAccessCardAccessLevel([FromBody] ModifyAccessCardLevelRequest request, CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.ModifyAccessLevel(_mapper.Map<AccessCard>(request), token)).ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("full")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAccessCardFull([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.GetById(id, token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAccessCard([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.GetById(id, token)).ToOk(c =>
                        new { c.Id, c.UserId, c.AccessLevel, c.LastUsingUtcDate });
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAccessCards(CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.GetAll(token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("paginated-search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaged([FromBody] PagedRequest request, CancellationToken token = default)
        {
            try
            {
                return (await _cardAccessService.GetPagedData(request,token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }
}
