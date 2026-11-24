using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Features.Devices.Dtos;
using AutoMapper;
using MediatR;

namespace SEYNT.Application.Features.Devices.Queries.GetDevices;

public sealed class GetDevicesQueryHandler(
    IDeviceRepository deviceRepository,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetDevicesQuery, List<DeviceResponseDto>>
{
    public async Task<List<DeviceResponseDto>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        var devices = await deviceRepository.GetAllAsync(currentUserService.OrganisationId, cancellationToken);
        return mapper.Map<List<DeviceResponseDto>>(devices);
    }
}
