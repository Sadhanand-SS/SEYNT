using FluentValidation;

namespace SEYNT.Application.Features.Devices.Commands.CreateDevice;

public sealed class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Device name is required.")
            .MaximumLength(100).WithMessage("Device name must not exceed 100 characters.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Device status is required.")
            .MaximumLength(50).WithMessage("Device status must not exceed 50 characters.");
    }
}
