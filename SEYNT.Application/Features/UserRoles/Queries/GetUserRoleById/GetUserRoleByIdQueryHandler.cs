using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Application.Features.UserRoles.Dtos;
using SEYNT.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRoleById;

public sealed class GetUserRoleByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetUserRoleByIdQuery, GetUserRolesResponseDto>
{
    public async Task<GetUserRolesResponseDto> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await context.UserRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.UserRoleId &&
                     x.RecordStatus != RecordStatus.Inactive &&
                     x.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (role is null)
            throw new NotFoundException("User role not found.");

        return mapper.Map<GetUserRolesResponseDto>(role);
    }
}
