using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: api/auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionReadDTO>>> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        // GET: api/auctions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionReadDTO>> GetAuctionById(Guid id)
        {
            var auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();

            return Ok(auction);
        }

        // POST: api/auctions
        [HttpPost]
        public async Task<ActionResult<AuctionReadDTO>> CreateAuction(AuctionCreateDTO auctionCreateDTO)
        {
            var auction = await _auctionService.CreateAuctionAsync(auctionCreateDTO);
            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.Id }, auction);
        }

        // PUT: api/auctions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(Guid id, AuctionUpdateDTO auctionUpdateDTO)
        {
            var updated = await _auctionService.UpdateAuctionAsync(id, auctionUpdateDTO);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/auctions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(Guid id)
        {
            var deleted = await _auctionService.DeleteAuctionAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
