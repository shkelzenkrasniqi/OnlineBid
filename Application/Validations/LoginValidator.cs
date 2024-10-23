using Domain.DTOs;
using FluentValidation;

namespace Application.Validations
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(dto => dto.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Email is not in the correct format.");

            RuleFor(dto => dto.Password)
               .NotEmpty().WithMessage("Password is required.");
        }
    }
}
