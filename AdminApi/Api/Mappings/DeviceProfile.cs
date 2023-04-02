using AdminApi.Models;
using AutoMapper;
using Domain.Entities;

namespace AdminApi.Mappings
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<GenerateDeviceRequest, Device>()
                .ForMember(t => t.Id, option => option.Ignore());

            CreateMap<ModifyDeviceLevelRequest, Device>()
                .ForMember(t => t.Description, option => option.Ignore());
        }
    }
}
