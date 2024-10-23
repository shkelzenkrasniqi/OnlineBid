using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineBid.Extensions;

namespace OnlineBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController(IBidService _bidService, IHubContext<BidHub> _hubContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidReadDTO>>> GetAllBids()
        {
            var bids = await _bidService.GetAllBidsAsync();
            return Ok(bids);
        }
        [HttpGet("{auctionId}/bids")]
        public async Task<ActionResult<IEnumerable<BidReadDTO>>> GetBidsForAuction(Guid auctionId)
        {
            var bids = await _bidService.GetAllBidsForAuctionAsync(auctionId);
            return Ok(bids);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BidReadDTO>> GetBidById(Guid id)
        {
            var bid = await _bidService.GetBidByIdAsync(id);
            if (bid == null) return NotFound();
            return Ok(bid);
        }

        [HttpPost]
        public async Task<ActionResult<BidReadDTO>> CreateBid([FromBody] BidCreateDTO bidCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bid = await _bidService.CreateBidAsync(bidCreateDTO);

            await _hubContext.Clients.All.SendAsync("ReceiveNewBid", bid.Amount, bid.BidTime, bid.UserId,bid.AuctionId );

            return CreatedAtAction(nameof(GetBidById), new { id = bid.Id }, bid);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            var success = await _bidService.DeleteBidAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
