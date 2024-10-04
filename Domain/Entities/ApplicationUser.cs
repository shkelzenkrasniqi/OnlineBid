using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Bid> Bids { get; set; } 
        public ICollection<Auction> Auctions { get; set; }
    }
}
