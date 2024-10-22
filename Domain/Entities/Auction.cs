using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? CurrentPrice { get; set; }
        public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } 
        public ICollection<Bid> Bids { get; set; }
        public List<AuctionPhoto> Photos { get; set; }

    }
}
