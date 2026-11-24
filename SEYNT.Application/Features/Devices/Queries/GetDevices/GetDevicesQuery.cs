using SEYNT.Application.Features.Devices.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Devices.Queries.GetDevices;

public sealed class GetDevicesQuery : IRequest<List<DeviceResponseDto>>
{
}
