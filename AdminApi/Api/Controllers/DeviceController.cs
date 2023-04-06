using AdminApi.Extensions;
using AdminApi.Models;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(IDeviceService deviceService, IMapper mapper)
        {
            _deviceService = deviceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] GenerateDeviceRequest request, CancellationToken token = default)
        {
            try
            {
                return (await _deviceService.Add(_mapper.Map<Device>(request), token)).ToOk(c => true);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDevice([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                return (await _deviceService.Remove(id, token)).ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("change-access")]
        public async Task<IActionResult> ModifyDeviceAccessLevel([FromQuery] ModifyDeviceLevelRequest request, CancellationToken token = default)
        {
            try
            {
                return (await _deviceService.ModifyAccessLevel(_mapper.Map<Device>(request), token)).ToOk(c => true);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceById([FromQuery] Guid id, CancellationToken token = default)
        {
            try
            {
                return (await _deviceService.GetById(id, token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDevices(CancellationToken token = default)
        {
            try
            {
                return (await _deviceService.GetAll(token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("paginated-search")]
        public async Task<IActionResult> GetPaged([FromBody] PagedRequest request, CancellationToken token = default)
        {
            try
            {
                return (await _deviceService.GetPagedData(request, token)).ToOk(c => c);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }
}
