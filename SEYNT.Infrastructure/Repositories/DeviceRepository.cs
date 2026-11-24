using SEYNT.Application.Common.Interfaces;
using SEYNT.Domain.Entities;
using SEYNT.Domain.Enums;
using SEYNT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Infrastructure.Repositories;

public class DeviceRepository(ApplicationDbContext context) : IDeviceRepository
{
    public async Task<DeviceEntity?> GetByIdAsync(Guid id, Guid organisationId, CancellationToken cancellationToken = default)
    {
        return await context.Devices
            .FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.RecordStatus == RecordStatus.Active &&
                     x.OrganisationId == organisationId,
                cancellationToken);
    }

    public async Task<List<DeviceEntity>> GetAllAsync(Guid organisationId, CancellationToken cancellationToken = default)
    {
        return await context.Devices
            .AsNoTracking()
            .Where(x => x.RecordStatus == RecordStatus.Active &&
                        x.OrganisationId == organisationId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<DeviceEntity> AddAsync(DeviceEntity device, CancellationToken cancellationToken = default)
    {
        context.Devices.Add(device);
        await context.SaveChangesAsync(cancellationToken);
        return device;
    }

    public async Task UpdateAsync(DeviceEntity device, CancellationToken cancellationToken = default)
    {
        context.Devices.Update(device);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceEntity device, CancellationToken cancellationToken = default)
    {
        context.Devices.Update(device);
        await context.SaveChangesAsync(cancellationToken);
    }
}
