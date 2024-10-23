﻿using Domain.DTOs;
using FluentValidation;

namespace Application.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(dto => dto.UserName)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not in the correct format.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }
    }
}
