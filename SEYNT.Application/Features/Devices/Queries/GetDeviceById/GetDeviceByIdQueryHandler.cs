using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Application.Features.Devices.Dtos;
using AutoMapper;
using MediatR;

namespace SEYNT.Application.Features.Devices.Queries.GetDeviceById;

public sealed class GetDeviceByIdQueryHandler(
    IDeviceRepository deviceRepository,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetDeviceByIdQuery, DeviceResponseDto>
{
    public async Task<DeviceResponseDto> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetByIdAsync(request.Id, currentUserService.OrganisationId, cancellationToken);

        if (device is null)
            throw new NotFoundException("Device not found.");

        return mapper.Map<DeviceResponseDto>(device);
    }
}
