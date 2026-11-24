using FluentValidation;
using TimeZoneConverter;

namespace SEYNT.Application.Features.Organisations.Commands.UpdateOrganisation;

public sealed class UpdateOrganisationCommandValidator : AbstractValidator<UpdateOrganisationCommand>
{
    public UpdateOrganisationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.OrganisationName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.TimeZoneId)
            .Must(tz => !string.IsNullOrWhiteSpace(tz) && TZConvert.TryGetTimeZoneInfo(tz, out _))
            .WithMessage("TimeZoneId must be a valid IANA time zone identifier.");
    }
}
