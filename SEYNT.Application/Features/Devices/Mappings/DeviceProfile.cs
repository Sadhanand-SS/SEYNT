using SEYNT.Application.Features.Devices.Dtos;
using SEYNT.Domain.Entities;
using AutoMapper;

namespace SEYNT.Application.Features.Devices.Mappings;

public sealed class DeviceProfile : Profile
{
    public DeviceProfile()
    {
        CreateMap<DeviceEntity, DeviceResponseDto>()
            .ForMember(
                dest => dest.RecordStatus,
                opt => opt.MapFrom(src => (byte)src.RecordStatus));
    }
}
