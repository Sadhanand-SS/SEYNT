using SEYNT.Application.Features.UserRoles.Dtos;
using SEYNT.Domain.Entities;
using AutoMapper;

namespace SEYNT.Application.Features.UserRoles.Mappings;

public sealed class UserRolesProfile : Profile
{
    public UserRolesProfile()
    {
        CreateMap<UserRoleEntity, GetUserRolesResponseDto>()
            .ForMember(
                dest => dest.RecordStatus,
                opt => opt.MapFrom(src => (byte)src.RecordStatus));
    }
}
