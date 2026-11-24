using SEYNT.Domain.Entities;

namespace SEYNT.Application.Common.Interfaces;

public interface IDeviceRepository
{
    Task<DeviceEntity?> GetByIdAsync(Guid id, Guid organisationId, CancellationToken cancellationToken = default);
    Task<List<DeviceEntity>> GetAllAsync(Guid organisationId, CancellationToken cancellationToken = default);
    Task<DeviceEntity> AddAsync(DeviceEntity device, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeviceEntity device, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeviceEntity device, CancellationToken cancellationToken = default);
}
