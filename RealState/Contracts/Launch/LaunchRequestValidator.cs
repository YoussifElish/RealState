using FluentValidation;

namespace RealState.Contracts.Launch;

public class LaunchRequestValidator : AbstractValidator<LaunchRequest>
{
    public LaunchRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.");

       
    }
}