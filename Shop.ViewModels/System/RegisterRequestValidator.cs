using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ViewModels.System
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.user).NotEmpty().WithMessage("UserName is required!");

            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required!")
                    .MaximumLength(50).WithMessage("Last name cannot over 50 characters");
            RuleFor(x => x.email).NotEmpty().WithMessage("Email is required")
                    .Matches(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]
|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\
uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$")
                    .WithMessage("Email fomat not match!");
            RuleFor(x => x.phone).NotEmpty().WithMessage("Phone number is required!")
                     .MaximumLength(10).WithMessage("Phone number cannot over 10 number");

            RuleFor(x => x.password).NotEmpty().WithMessage("Password isn't empty!")
                    .MinimumLength(6).WithMessage("Password is at least 6 charatcers")
                    .MaximumLength(20).WithMessage("Passord cann't over 20 characters");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.password != request.confirmpassword)
                {
                    context.AddFailure("Confirm password is not match!");
                }
            });
        }
    }
}
