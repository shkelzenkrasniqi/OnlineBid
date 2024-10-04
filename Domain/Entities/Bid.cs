using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bid
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
        public Guid UserId { get; set; }  
        public ApplicationUser User { get; set; }
        public Guid AuctionId { get; set; }  
        public Auction Auction { get; set; }
    }
}
