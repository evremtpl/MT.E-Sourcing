using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Sourcing.Core.Entities;
using MT.E_Sourcing.Sourcing.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;
        private readonly ILogger _logger;

        public BidController(IBidService bidService, ILogger logger)
        {
            _bidService = bidService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> SendBid([FromBody] Bid bid)
        {
            await _bidService.SendBid(bid);

            return Ok();
        }

        [HttpGet(Name = "GetBidByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidByAuctionId(string id)
        {
            var bids = await _bidService.GetBidByAuctionId(id);

            if(!bids.Any())
            {
                _logger.LogError($" Bids with auctionId {id}, has not beem in database");
                return BadRequest($" Bids with auctionId {id}, has not been in database");
            }
            return (bids);
        }
        [HttpGet(Name ="GetWinnerBid")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Bid>> GetWinnerBid (string id)
        {
            var bid = await _bidService.GetWinnerBid(id);

            return Ok(bid);
        }
    }
}
