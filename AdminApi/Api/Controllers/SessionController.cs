using AdminApi.Extensions;
using AdminApi.Models;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

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
                                                              CancellationToken cancellationToken = default)
        {
            try
            {
                var session = _mapper.Map<Session>(request);
                session.Id = Guid.NewGuid();
                var result = await _sessionService.Add(session, cancellationToken);

                if (result.IsSuccess)
                {
                    _syncService.AddSession(session);
                }

                return result.ToOk(c => c);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSessionWithoutDevice([FromBody] GenerateSessionRequestByAdminWithoutDevice request,
                                                                 CancellationToken cancellationToken = default)
        {
            try
            {
                var session = _mapper.Map<Session>(request);
                session.Id = Guid.NewGuid();
                var result = await _sessionService.Add(session, cancellationToken);

                if (result.IsSuccess)
                {
                    _syncService.AddSession(session);
                }

                return result.ToOk(c => c);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSession([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _sessionService.Remove(id, cancellationToken);

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

        public async Task<IActionResult> ActivateSession([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _sessionService.Use(id, cancellationToken);

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
        public async Task<IActionResult> GetAllSessions(CancellationToken cancellationToken = default)
        {
            try
            {
                return (await _sessionService.GetAll(cancellationToken)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("close")]
        [Authorize(Roles = "Admin, Supervizer")]
        public async Task<IActionResult> CloseSession([FromQuery] CloseSessionRequestBySessionId request,
                                                      CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _sessionService.CloseSession(request.Id, cancellationToken);

                return result.ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("paginated-search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaged([FromBody] PagedRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                return (await _sessionService.GetPagedData(request, cancellationToken)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }
}
