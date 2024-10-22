using Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class AuctionValidator : AbstractValidator<AuctionCreateDTO>
    {
        public AuctionValidator()
        {
            RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Auction Titile cannot be empty!")
                    .MaximumLength(50).WithMessage("Auction Title cannot have more than 50 characters!");
            RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Description can not be empty!")
                    .MaximumLength(700).WithMessage("Description cannot have more than 700 characters!");
            RuleFor(x => x.StartingPrice)
                    .NotEmpty().WithMessage("Auction Titile can not be empty!")
                    .GreaterThan(0).WithMessage("Starting price must be more than 0!");
            RuleFor(x => x.StartDate)
                    .NotEmpty().WithMessage("You should select a start date")
                    .Must(BeAValidDate).WithMessage("Start date must be a valid date");

            RuleFor(x => x.EndDate)
                    .NotEmpty().WithMessage("You should select an end date")
                    .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date")
                    .Must(BeAValidDate).WithMessage("End date must be a valid date");

            RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("User ID is required")
                    .NotEqual(Guid.Empty).WithMessage("User ID is invalid");

            RuleFor(x => x.CategoryId)
                    .NotNull().WithMessage("Category cannot be null");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}