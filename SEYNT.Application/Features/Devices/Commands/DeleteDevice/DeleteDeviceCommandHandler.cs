using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;

namespace SEYNT.Application.Features.Devices.Commands.DeleteDevice;

public sealed class DeleteDeviceCommandHandler(
    IDeviceRepository deviceRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteDeviceCommand, Guid>
{
    public async Task<Guid> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetByIdAsync(request.DeviceId, currentUserService.OrganisationId, cancellationToken);

        if (device is null)
            throw new NotFoundException("Device not found.");

        device.RecordStatus = RecordStatus.Inactive;
        device.UpdatedBy = currentUserService.UserId;

        await deviceRepository.DeleteAsync(device, cancellationToken);

        return device.Id;
    }
}
