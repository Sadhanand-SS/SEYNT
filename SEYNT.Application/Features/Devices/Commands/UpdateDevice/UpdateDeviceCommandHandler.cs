using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using MediatR;

namespace SEYNT.Application.Features.Devices.Commands.UpdateDevice;

public sealed class UpdateDeviceCommandHandler(
    IDeviceRepository deviceRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateDeviceCommand, Guid>
{
    public async Task<Guid> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetByIdAsync(request.Id, currentUserService.OrganisationId, cancellationToken);

        if (device is null)
            throw new NotFoundException("Device not found.");

        device.Name = request.Name;
        device.Status = request.Status;
        device.UpdatedBy = currentUserService.UserId;

        await deviceRepository.UpdateAsync(device, cancellationToken);

        return device.Id;
    }
}
