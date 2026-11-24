using FluentValidation;

namespace SEYNT.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.UserRoleId)
            .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .When(x => !string.IsNullOrWhiteSpace(x.Password));
    }
}
