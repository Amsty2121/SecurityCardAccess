using AdminApi.Extensions;
using AdminApi.Models;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ISyncService _syncService;

        private readonly IMapper _mapper;

        public SessionController(ISessionService sessionService, IMapper mapper, ISyncService syncService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _syncService = syncService;
        }

        [HttpPost("withDevice")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSessionWithDevice([FromBody] GenerateSessionRequestByAdminWithDevice request,
                                                              CancellationToken token = default)
        {
            try
            {
                var session = _mapper.Map<Session>(request);
                session.Id = Guid.NewGuid();
                var result = await _sessionService.Add(session, token);

                if (result.IsSuccess)
                {
                    _syncService.AddSession(session);
                }

                return result.ToOk(c => true);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSessionWithoutDevice([FromBody] GenerateSessionRequestByAdminWithoutDevice request,
                                                                 CancellationToken token = default)
        {
            try
            {
                var session = _mapper.Map<Session>(request);
                session.Id = Guid.NewGuid();
                var result = await _sessionService.Add(session, token);

                if (result.IsSuccess)
                {
                    _syncService.AddSession(session);
                }

                return result.ToOk(c => true);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSession([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _sessionService.Remove(id, token);

                if (result.IsSuccess)
                {
                    _syncService.DeleteSession(id);
                }

                return result.ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("activate")]
        [Authorize(Roles = "User, Admin")]

        public async Task<IActionResult> ActivateSession([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _sessionService.Use(id, token);

                if (result.IsSuccess)
                {
                    _syncService.UseSession(id);
                }

                return result.ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSessions(CancellationToken token = default)
        {
            try
            {
                return (await _sessionService.GetAll(token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("close")]
        public async Task<IActionResult> CloseSession([FromBody] CloseSessionRequestById request,
                                                      CancellationToken token = default)
        {
            try
            {
                var result = await _sessionService.CloseSession(request.Id, token);

                return result.ToOk(c => true);
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
                return (await _sessionService.GetPagedData(request, token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }
}
