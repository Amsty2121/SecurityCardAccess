using AdminApi.Extensions;
using AdminApi.Models;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Models.PagedRequest;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Interfaces;
using System.Threading;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ISyncService _syncService;
		private readonly IGenericRepository<SessionContext, Session> _sessionRepository;

		private readonly IMapper _mapper;

		public SessionController(ISessionService sessionService, IMapper mapper, ISyncService syncService, IGenericRepository<SessionContext, Session> sessionRepository)
		{
			_sessionService = sessionService;
			_mapper = mapper;
			_syncService = syncService;
			_sessionRepository = sessionRepository;
		}

		[HttpPost("withDevice")]
        public async Task<IActionResult> AddSessionWithDevice([FromBody] GenerateSessionRequestByAdminWithDevice request,
                                                              CancellationToken cancellationToken = default)
        {
            try
            {
                var session = _mapper.Map<Session>(request);
                session.Id = Guid.NewGuid();
				Result<Session> result;
				if (request.IsFizic)
				{
					result = await _sessionService.Add(session, cancellationToken);
				}
				else
				{
					result = await _sessionService.AddFizicCard(session, cancellationToken);
				}

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
        public async Task<IActionResult> CloseSession([FromBody] CloseSessionRequestBySessionId request,
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

		[HttpGet("closeByDevice")]
		public async Task<IActionResult> CloseSessionByDevice([FromBody] GenerateSessionRequestByAdminWithDevice request,
													  CancellationToken cancellationToken = default)
		{
			try
			{
                var sessions = await _sessionRepository.GetWhere(x => x.AccessCardId.Equals(request.AccessCardId) && x.DeviceId.Equals(request.DeviceId) && x.SessionStatus == SessionStatus.Active);

                if (!sessions.Any())
                    return BadRequest();

                var result = await _sessionService.CloseSession(sessions.First().Id, cancellationToken);

				return result.ToOk(c => true);
			}
			catch (Exception)
			{
				return Unauthorized();
			}
		}

		[HttpPost("paginated-search")]
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
