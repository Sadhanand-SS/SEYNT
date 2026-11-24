using MediatR;

namespace SEYNT.Application.Features.Devices.Commands.UpdateDevice;

public sealed class UpdateDeviceCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
