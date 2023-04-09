using Application.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.PagedRequest;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IGenericRepository<SessionContext, Device> _deviceRepository;
        public DeviceService(IGenericRepository<SessionContext, Device> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result<Device>> Add(Device device, CancellationToken cancellationToken = default)
        {
            device.Id = Guid.NewGuid();
            await _deviceRepository.Add(device, cancellationToken);
            
            return new Result<Device>(device);
        }

        public async Task<Result<IEnumerable<Device>>> GetAll(CancellationToken cancellationToken = default) 
            => new Result<IEnumerable<Device>>(await _deviceRepository.GetAll(cancellationToken));

        public async Task<Result<Device>> GetById(Guid Id, CancellationToken cancellationToken = default)
        {
            var device = await _deviceRepository.GetById(Id, cancellationToken);

            return device == null ? 
                new Result<Device>(new DeviceNotFoundException("Device not found")) : 
                new Result<Device>(device);
        }

        public async Task<Result<PaginatedResult<Device>>> GetPagedData(PagedRequest request, CancellationToken cancellationToken = default)
        {
            return new Result<PaginatedResult<Device>>(await _deviceRepository.GetPagedData<Device>(request));

        }

        public async Task<Result<bool>> ModifyAccessLevel(Device deviceToUpdate, CancellationToken cancellationToken = default)
        {
            var device = await _deviceRepository.GetById(deviceToUpdate.Id, cancellationToken);

            if (device == null)
            {
                return new Result<bool>(new DeviceNotFoundException("Device not found"));
            }

            device.AccessLevel = deviceToUpdate.AccessLevel;

            await _deviceRepository.Update(device, cancellationToken);

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> Remove(Guid id, CancellationToken cancellationToken = default)
        {
            var device = await _deviceRepository.GetById(id, cancellationToken);

            if (device == null)
            {
                return new Result<bool>(new DeviceNotFoundException("Device not found"));
            }

            await _deviceRepository.Remove(device, cancellationToken);

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> Update(Device deviceToUpdate, CancellationToken cancellationToken = default)
        {
            var device = await _deviceRepository.GetById(deviceToUpdate.Id, cancellationToken);

            if (device == null)
            {
                return new Result<bool>(new DeviceNotFoundException("Device not found"));
            }

            device.AccessLevel = deviceToUpdate.AccessLevel;

            await _deviceRepository.Update(device, cancellationToken);

            return new Result<bool>(true);
        }
    }
}
