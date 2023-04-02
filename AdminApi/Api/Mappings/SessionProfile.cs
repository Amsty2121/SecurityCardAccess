using AdminApi.Models;
using AutoMapper;
using Domain.Entities;

namespace AdminApi.Mappings
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<GenerateSessionRequestByUser, Session>()
                .ForMember(t => t.Id, option => option.Ignore())
                .ForMember(t => t.UserId, option => option.Ignore())
                .ForMember(t => t.DeviceId, option => option.Ignore())
                .ForMember(t => t.SessionStatus, option => option.Ignore())
                .ForMember(t => t.EndUtcDate, option => option.Ignore())
                .ForMember(t => t.StartUtcDate, option => option.Ignore())
                .ForMember(t => t.UsedUtcDate, option => option.Ignore());

            CreateMap<GenerateSessionRequestByAdminWithoutDevice, Session>()
                .ForMember(t => t.Id, option => option.Ignore())
                .ForMember(t => t.DeviceId, option => option.Ignore())
                .ForMember(t => t.SessionStatus, option => option.Ignore())
                .ForMember(t => t.EndUtcDate, option => option.Ignore())
                .ForMember(t => t.StartUtcDate, option => option.Ignore())
                .ForMember(t => t.UsedUtcDate, option => option.Ignore());

            CreateMap<GenerateSessionRequestByAdminWithDevice, Session>()
                .ForMember(t => t.Id, option => option.Ignore())
                .ForMember(t => t.SessionStatus, option => option.Ignore())
                .ForMember(t => t.EndUtcDate, option => option.Ignore())
                .ForMember(t => t.StartUtcDate, option => option.Ignore())
                .ForMember(t => t.UsedUtcDate, option => option.Ignore());

        }
    }
}
