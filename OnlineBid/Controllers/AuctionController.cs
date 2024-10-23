using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController(IAuctionService auctionService) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IActionResult> SearchAuctions([FromQuery] string searchTerm)
        {
            var auctions = await auctionService.SearchAuctionsAsync(searchTerm);
            return Ok(auctions);
        }
        // GET: api/auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionReadDTO>>> GetAllAuctions()
        {
            var auctions = await auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        // GET: api/auctions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionReadDTO>> GetAuctionById(Guid id)
        {
            var auction = await auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();

            return Ok(auction);
        }
       
        // POST: api/auctions
        [HttpPost]
        public async Task<ActionResult<AuctionReadDTO>> CreateAuction([FromForm] AuctionCreateDTO auctionCreateDTO, [FromForm] List<IFormFile> photos)
        {
            var auction = await auctionService.CreateAuctionAsync(auctionCreateDTO, photos);
            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.Id }, auction);
        }
     
        // PUT: api/auctions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(Guid id, AuctionUpdateDTO auctionUpdateDTO)
        {
            var updated = await auctionService.UpdateAuctionAsync(id, auctionUpdateDTO);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/auctions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(Guid id)
        {
            var deleted = await auctionService.DeleteAuctionAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
