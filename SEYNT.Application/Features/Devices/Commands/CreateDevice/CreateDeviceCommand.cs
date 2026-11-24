using MediatR;

namespace SEYNT.Application.Features.Devices.Commands.CreateDevice;

public sealed class CreateDeviceCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
}
