using FluentValidation;

namespace SEYNT.Application.Features.Devices.Commands.UpdateDevice;

public sealed class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Device id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Device name is required.")
            .MaximumLength(100).WithMessage("Device name must not exceed 100 characters.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Device status is required.")
            .MaximumLength(50).WithMessage("Device status must not exceed 50 characters.");
    }
}
