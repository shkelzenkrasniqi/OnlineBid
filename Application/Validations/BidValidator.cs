using Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class BidValidator : AbstractValidator<BidCreateDTO>
    {
        public BidValidator() {
            RuleFor(x => x.Amount)
                    .NotEmpty().WithMessage("Bidding amount cannot be empty");
            RuleFor(x => x.BidTime)
                    .NotEmpty().WithMessage("Bid time cannot be empty")
                    .Must(BeAValidDate).WithMessage("Bid time must be a valid date");

            RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("User ID is required")
                    .NotEqual(Guid.Empty).WithMessage("User ID is invalid");

            RuleFor(x => x.AuctionId)
                    .NotEmpty().WithMessage("Auction ID is required")
                    .NotEqual(Guid.Empty).WithMessage("Auction ID is invalid");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
