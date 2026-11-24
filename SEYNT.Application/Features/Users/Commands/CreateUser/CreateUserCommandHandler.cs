using SEYNT.Application.Common.Extensions;
using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Domain.Entities;
using SEYNT.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateUserCommand, CreateUserResponseDto>
{
    public async Task<CreateUserResponseDto> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.NormalizeEmail();

        var existingUser = await context.Users
            .AnyAsync(
                u => u.Email.ToLower() == email && u.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (existingUser)
            throw new ConflictException("User with this email already exists.");

        var roleExists = await context.UserRoles
            .AnyAsync(
                r => r.Id == request.UserRoleId && r.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (!roleExists)
            throw new NotFoundException("User role not found.");

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            UserRoleId = request.UserRoleId,
            RecordStatus = RecordStatus.Active,
            CreatedBy = currentUserService.UserId,
            OrganisationId = currentUserService.OrganisationId,
            UpdatedBy = currentUserService.UserId
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<CreateUserResponseDto>(user);
    }
}
