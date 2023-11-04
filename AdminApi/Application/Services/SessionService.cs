using Application.IServices;
using Domain.Entities;
using Repository.Interfaces;
using Repository;
using LanguageExt.Common;
using Domain.Exceptions;
using System.Linq.Dynamic.Core;
using Domain.Models.PagedRequest;
using System.Threading;

namespace Application.Services
{
    public class SessionService : ISessionService
    {


        private readonly IGenericRepository<SessionContext, Session> _sessionRepository;
        public SessionService(IGenericRepository<SessionContext, Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<Session>> Add(Session session, CancellationToken cancellationToken = default)
        {
            if (IsTheSameSessionActive(session, cancellationToken))
            {
                return new Result<Session>(new SessionIsAlreadyActiveException("Session is already active"));
            }
            session.Id = Guid.NewGuid();
            session.StartUtcDate = DateTime.UtcNow;
            //sessionToActivate.EndUtcDate = DateTime.UtcNow + TimeSpan.FromMinutes(5);
            session.EndUtcDate = DateTime.UtcNow + TimeSpan.FromSeconds(60);
            session.SessionStatus = SessionStatus.Active;
            session.UsedUtcDate = null;

            await _sessionRepository.Add(session, cancellationToken);

            return new Result<Session>(session);
        }

		public async Task<Result<Session>> AddFizicCard(Session session, CancellationToken cancellationToken = default)
		{
			session.Id = Guid.NewGuid();
			session.StartUtcDate = DateTime.UtcNow;
			//sessionToActivate.EndUtcDate = DateTime.UtcNow + TimeSpan.FromMinutes(5);
			session.EndUtcDate = DateTime.UtcNow + TimeSpan.FromSeconds(60);
			session.SessionStatus = SessionStatus.Closed;
			session.UsedUtcDate = null;

			await _sessionRepository.Add(session, cancellationToken);

			return new Result<Session>(session);
		}

		public async Task<Result<IEnumerable<Session>>> GetAll(CancellationToken cancellationToken = default)
        {
            var sessions = _sessionRepository.GetAll(cancellationToken).Result;

            sessions.ToList().ForEach(session =>
			{
				if (session.EndUtcDate < DateTime.UtcNow && session.SessionStatus == SessionStatus.Active)
				{
					session.SessionStatus = SessionStatus.Closed;
				}
			});

            return new Result<IEnumerable<Session>>(sessions);
        }

        public async Task<Result<Session>> GetById(Guid Id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionRepository.GetById(Id, cancellationToken);

            return session == null ?
                new Result<Session>(new DeviceNotFoundException("Session not found")) :
                new Result<Session>(session);
        }

        public async Task<Result<bool>> Remove(Guid id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionRepository.GetById(id, cancellationToken);

            if (session == null)
            {
                return new Result<bool>(new SessionNotFoundException("Session not found"));
            }

            if (!IsSessionActive(session))
            {
                return new Result<bool>(new SessionIsAlreadyClosedException("Session already closed"));
            }

            session.SessionStatus = SessionStatus.Closed;

            await _sessionRepository.Update(session, cancellationToken);
            return new Result<bool>(true);
        }

        public async Task<Result<bool>> Use(Guid id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionRepository.GetById(id, cancellationToken);

            if (session == null)
            {
                return new Result<bool>(new SessionNotFoundException("Session not found"));
            }

            if (!IsSessionActive(session))
            {
                return new Result<bool>(new SessionIsAlreadyClosedException("Session already closed"));
            }

            session.SessionStatus = SessionStatus.Closed;
            session.UsedUtcDate = DateTime.UtcNow;

            await _sessionRepository.Update(session, cancellationToken);
            return new Result<bool>(true);
        }

        public async Task<Result<bool>> CloseSession(Guid id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionRepository.GetById(id, cancellationToken);

            if (session == null)
            {
                return new Result<bool>(new SessionNotFoundException("Session not found"));
            }

            if (session.SessionStatus != SessionStatus.Active)
            {
                return new Result<bool>(new SessionIsAlreadyClosedException("Session already closed"));
            }

            session.SessionStatus = SessionStatus.Closed;

            await _sessionRepository.Update(session, cancellationToken);
            return new Result<bool>(true);

        }

        public async Task<Result<PaginatedResult<Session>>> GetPagedData(PagedRequest request, CancellationToken cancellationToken = default)
        {
            return new Result<PaginatedResult<Session>>(await _sessionRepository.GetPagedData<Session>(request));
        }

        private bool IsSessionActive(Session session)
            => session.UsedUtcDate == null &&
               session.EndUtcDate > DateTime.UtcNow &&
               session.SessionStatus == SessionStatus.Active ? true : false;

        private bool IsTheSameSessionActive(Session session, CancellationToken cancellationToken)
        {
            var validSession = _sessionRepository.GetWhere(x => x.AccessCardId == session.AccessCardId &&
                                                            //x.DeviceId == null &&
                                                            x.SessionStatus == SessionStatus.Active &&
                                                            x.UsedUtcDate == null &&
                                                            x.EndUtcDate > DateTime.UtcNow, cancellationToken).Result.ToList().OrderBy(x => x.EndUtcDate).LastOrDefault();
            return validSession != null;
        }


	}
}
