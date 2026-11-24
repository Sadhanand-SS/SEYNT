using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetUserByIdQuery, GetUsersResponseDto>
{
    public async Task<GetUsersResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(x => x.UserRole)
            .FirstOrDefaultAsync(
                x => x.Id == request.UserId &&
                     x.RecordStatus != RecordStatus.Inactive &&
                     x.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found.");

        return mapper.Map<GetUsersResponseDto>(user);
    }
}
