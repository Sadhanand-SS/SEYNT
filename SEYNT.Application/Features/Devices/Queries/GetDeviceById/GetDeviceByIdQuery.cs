using SEYNT.Application.Features.Devices.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Devices.Queries.GetDeviceById;

public sealed class GetDeviceByIdQuery : IRequest<DeviceResponseDto>
{
    public Guid Id { get; set; }
}
