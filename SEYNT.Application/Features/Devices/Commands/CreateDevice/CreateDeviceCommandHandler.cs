using SEYNT.Application.Common.Interfaces;
using SEYNT.Domain.Entities;
using SEYNT.Domain.Enums;
using MediatR;

namespace SEYNT.Application.Features.Devices.Commands.CreateDevice;

public sealed class CreateDeviceCommandHandler(
    IDeviceRepository deviceRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<CreateDeviceCommand, Guid>
{
    public async Task<Guid> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = new DeviceEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Status = request.Status,
            RecordStatus = RecordStatus.Active,
            OrganisationId = currentUserService.OrganisationId,
            CreatedBy = currentUserService.UserId,
            UpdatedBy = currentUserService.UserId
        };

        await deviceRepository.AddAsync(device, cancellationToken);

        return device.Id;
    }
}
