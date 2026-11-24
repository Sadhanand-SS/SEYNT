using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Domain.Entities;
using AutoMapper;

namespace SEYNT.Application.Features.Users.Mapping;

public sealed class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<UserEntity, GetUsersResponseDto>()
            .ForMember(
                dest => dest.RecordStatus,
                opt => opt.MapFrom(src => (byte)src.RecordStatus))
            .ForMember(
                dest => dest.RoleName,
                opt => opt.MapFrom(src => src.UserRole.Name));

        CreateMap<UserEntity, CreateUserResponseDto>();
    }
}
