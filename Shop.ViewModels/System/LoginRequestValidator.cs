using FluentValidation;

namespace Shop.ViewModels.System
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Account).NotEmpty().WithMessage("UserName is required!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password isn't empty!")
                    .MinimumLength(6).WithMessage("Password is at least 6 charatcers")
                    .MaximumLength(20).WithMessage("Password cannot over 20 characters");
        }
    }
}