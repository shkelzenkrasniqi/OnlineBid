using Microsoft.AspNetCore.SignalR;

namespace OnlineBid.Extensions
{
    public class BidHub : Hub
    {
        public async Task BroadcastNewBid(decimal amount,DateTime bidTime, string userId,string auctionId)
        {
            await Clients.All.SendAsync("ReceiveNewBid",amount,bidTime, userId,auctionId );
        }
    }
}
