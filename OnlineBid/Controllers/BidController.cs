using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineBid.Extensions;
using System.Web.Http.ModelBinding;

namespace OnlineBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;
        private readonly IHubContext<BidHub> _hubContext;

        public BidController(IBidService bidService, IHubContext<BidHub> hubContext)
        {
            _bidService = bidService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidReadDTO>>> GetAllBids()
        {
            var bids = await _bidService.GetAllBidsAsync();
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

            await _hubContext.Clients.All.SendAsync("ReceiveNewBid", bid.AuctionId, bid.UserId, bid.Amount);

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
