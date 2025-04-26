using RealState.Contracts.Auth;
using FluentValidation;

namespace RealState.Contracts.Auth;

public class LoginValidator : AbstractValidator<Loginrequest>
{
    public LoginValidator()
    {
        RuleFor(x=>x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x=>x.Password).NotEmpty();
    }
}
