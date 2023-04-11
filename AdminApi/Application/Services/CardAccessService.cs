using Application.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.PagedRequest;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Repository;
using Repository.Interfaces;
using System.Security.Claims;

namespace Application.Services
{
	public class CardAccessService : ICardAccessService
    {
        private readonly IGenericRepository<SessionContext, AccessCard> _cardRepository;
        private readonly UserManager<User> _userManager;
        public CardAccessService(IGenericRepository<SessionContext, AccessCard> cardRepository, UserManager<User> userManager)
        {
            _cardRepository = cardRepository;
            _userManager = userManager;
        }

        public async Task<Result<AccessCard>> Add(AccessCard accessCard, CancellationToken cancellationToken = default)
        {
            if(await IsUserValid(accessCard.UserId))
            {
                return new Result<AccessCard>(new AccountNotFoundException("Account not found"));
            }

            accessCard.Id = Guid.NewGuid();   
            await _cardRepository.Add(accessCard, cancellationToken);

            return new Result<AccessCard>(accessCard);
        }

        public async Task<Result<IEnumerable<AccessCard>>> GetAll(CancellationToken cancellationToken = default)
            => new Result<IEnumerable<AccessCard>>(await _cardRepository.GetAll());

        public async Task<Result<AccessCard>> GetById(Guid Id, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetById(Id, cancellationToken);

            return card == null ?
                new Result<AccessCard>(new CardNotFoundException("Card not found")) :
                new Result<AccessCard>(card);
        }

        public async Task<Result<PaginatedResult<AccessCard>>> GetPagedData(PagedRequest request, CancellationToken cancellationToken = default)
        {
            return new Result<PaginatedResult<AccessCard>>(await _cardRepository.GetPagedData<AccessCard>(request, cancellationToken));
        }

        public async Task<Result<bool>> ModifyAccessLevel(AccessCard accessCardToUpdate, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetById(accessCardToUpdate.Id, cancellationToken);

            if (card == null)
            {
                return new Result<bool>(new CardNotFoundException("Card not found"));
            }

            card.AccessLevel = accessCardToUpdate.AccessLevel;

            await _cardRepository.Update(card, cancellationToken);

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> Remove(Guid id, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetById(id, cancellationToken);

            if (card == null)
            {
                return new Result<bool>(new CardNotFoundException("Card not found"));
            }

            await _cardRepository.Remove(card, cancellationToken);

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> Update(AccessCard accessCardToUpdate, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetById(accessCardToUpdate.Id, cancellationToken);

            if (card == null)
            {
                return new Result<bool>(new CardNotFoundException("Card not found"));
            }

            card.AccessLevel = accessCardToUpdate.AccessLevel;

            await _cardRepository.Update(card, cancellationToken);

            return new Result<bool>(true);
        }

        private async Task<User?> GetUserById(Guid id)
             => await _userManager.GetUserAsync(new ClaimsPrincipal(new List<ClaimsIdentity>() { new ClaimsIdentity(new List<Claim>() { new Claim(nameof(User.Id), id.ToString()) }) }));

        private async Task<bool> IsUserValid(Guid id) => await GetUserById(id) != null;
    }
}
