using Domain.Entities;
using Domain.Models.PagedRequest;
using LanguageExt.Common;

namespace Application.IServices
{
    public interface IDeviceService
    {
        //Admin
        Task<Result<Device>> Add(Device device, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<Device>>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<Device>> GetById(Guid Id, CancellationToken cancellationToken = default);
        Task<Result<bool>> ModifyAccessLevel(Device deviceToUpdate, CancellationToken cancellationToken = default);
        Task<Result<bool>> Remove(Guid id, CancellationToken cancellationToken = default);
        Task<Result<PaginatedResult<Device>>> GetPagedData(PagedRequest request, CancellationToken cancellationToken = default);
    }
}
