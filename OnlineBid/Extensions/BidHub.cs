using Microsoft.AspNetCore.SignalR;

namespace OnlineBid.Extensions
{
    public class BidHub : Hub
    {
        public async Task BroadcastNewBid(string auctionId, string userId, decimal Amount)
        {
            await Clients.All.SendAsync("ReceiveNewBid", auctionId, userId, Amount);
        }
    }
}
