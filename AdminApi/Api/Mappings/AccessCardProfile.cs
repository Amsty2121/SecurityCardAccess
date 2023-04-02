using AdminApi.Models;
using AutoMapper;
using Domain.Entities;

namespace AdminApi.Mappings
{
    public class AccessCardProfile : Profile
    {
        public AccessCardProfile()
        {
            CreateMap<GenerateAccessCardRequest, AccessCard>()
                .ForMember(t => t.Id, option => option.Ignore())
                .ForMember(t => t.LastUsingUtcDate, option => option.Ignore())
                .ForMember(t => t.CreateUtcDate, option => option.Ignore())
                .ForMember(t => t.UpdateUtcDate, option => option.Ignore());

            CreateMap<ModifyAccessCardLevelRequest, AccessCard>()
                .ForMember(t => t.Description, option => option.Ignore())
                .ForMember(t => t.LastUsingUtcDate, option => option.Ignore())
                .ForMember(t => t.CreateUtcDate, option => option.Ignore())
                .ForMember(t => t.UpdateUtcDate, option => option.Ignore())
                .ForMember(t => t.UserId, option => option.Ignore());
        }
    }
}
