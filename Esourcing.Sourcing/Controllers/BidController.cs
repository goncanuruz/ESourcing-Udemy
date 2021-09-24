using Esourcing.Sourcing.Entities;
using Esourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<AuctionController> _logger;

        public BidController(IBidRepository bidRepository, ILogger<AuctionController> logger)
        {
            _bidRepository = bidRepository;
            _logger = logger;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Bid),(int)(HttpStatusCode.OK))]
        public async Task<ActionResult<Bid>> SendBid(Bid bid)
        {
             await _bidRepository.SendBid(bid);
            return CreatedAtRoute("GetBid", new { id=bid.Id},bid);
        }
        [HttpGet("GetBidsByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidsByAuctionId(string auctionId)
        {
            return Ok(await _bidRepository.GetBidsByAuctionId(auctionId));
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(Bid), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<Bid>> GetWinnerBid(string auctionId)
        {
            return Ok(await _bidRepository.GetWinnerBid(auctionId));
        }

        [HttpGet("{id:length(24)}", Name = "GetBid")]
        [ProducesResponseType(typeof(Bid), (int)(HttpStatusCode.OK))]
        [ProducesResponseType((int)(HttpStatusCode.NotFound))]
        public async Task<ActionResult<Bid>> GetBid(string id)
        {
            var bid = await _bidRepository.GetBid(id);
            if (bid == null)
            {
                _logger.LogError("Auction with id :{id},hasn't been found in database.");
                return NotFound();
            }
            return Ok(bid);
        }
    }
}
