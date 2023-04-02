using AdminApi.Models;
using AutoMapper;
using Domain.Entities;


namespace AdminApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(t => t.Id, option => option.Ignore())
                .ForMember(t => t.NormalizedUserName, option => option.Ignore())
                .ForMember(t => t.Email, option => option.Ignore())
                .ForMember(t => t.NormalizedEmail, option => option.Ignore())
                .ForMember(t => t.EmailConfirmed, option => option.Ignore())
                .ForMember(t => t.PasswordHash, option => option.Ignore())
                .ForMember(t => t.SecurityStamp, option => option.Ignore())
                .ForMember(t => t.ConcurrencyStamp, option => option.Ignore())
                .ForMember(t => t.PhoneNumber, option => option.Ignore())
                .ForMember(t => t.PhoneNumberConfirmed, option => option.Ignore())
                .ForMember(t => t.TwoFactorEnabled, option => option.Ignore())
                .ForMember(t => t.LockoutEnd, option => option.Ignore())
                .ForMember(t => t.LockoutEnabled, option => option.Ignore())
                .ForMember(t => t.AccessFailedCount, option => option.Ignore());
        }
    }
}