using MediatR;

namespace SEYNT.Application.Features.Devices.Commands.DeleteDevice;

public sealed class DeleteDeviceCommand : IRequest<Guid>
{
    public Guid DeviceId { get; set; }
}
