using FluentValidation;

namespace SEYNT.Application.Features.UserRoles.Commands.CreateUserRole;

public sealed class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
{
    public CreateUserRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.UserRights)
            .NotEmpty();
    }
}
