using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Sourcing.Core.Entities;
using MT.E_Sourcing.Sourcing.Service.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;
      //  private readonly EventBusRabbitMqProducer _eventBus;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IAuctionService auctionService, IBidService bidService, IMapper mapper,ILogger<AuctionController> logger)
        {
            _auctionService = auctionService;
            _bidService = bidService;
            _mapper = mapper;
          //  _eventBus = eventBus;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
            return Ok(await _auctionService.GetAuctions());
        }

        [HttpGet("{id:length(24)}",Name = "GetAuction")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> GetAuction(string id)
        {
            var auction = await _auctionService.GetAuction(id);
            if (auction == null)
            {
                _logger.LogError($" Auction with id {id}, has not beem in database");
                return NotFound();
            }
            return Ok(auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> Add([FromBody] Auction auction)
        {
            await _auctionService.Add(auction);
            return CreatedAtRoute("GetAuction", new { id = auction.Id }, auction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> Update([FromBody] Auction auction)
        {
            await _auctionService.Update(auction);
            return CreatedAtRoute("GetAuction", new { id = auction.Id }, auction);
        }
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Delete(string id)
        {
            var auction = _auctionService.GetAuction(id).Result;

            if (auction != null)
            {
                await _auctionService.Delete(id);
                return NoContent();
            }
            return BadRequest($" Auction with id {id}, has not been in database");
        }
    //    [HttpPost(Name ="CompleteAuction")]
    //    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    //    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //    public async Task<ActionResult> CompleteAuction(string id)
    //    {
    //        Auction auction = await _auctionService.GetAuction(id);
    //        if(auction==null)
    //            return NotFound();
    //        if(auction.Status!=(int)Status.Active)
    //        {
    //            _logger.LogError("Auction is not active");
    //            return BadRequest();
    //        }

    //        Bid bid = await _bidService.GetWinnerBid(id);
    //        if (bid == null)
    //            return NotFound();

    //        OrderCreateEvent eventMessage = _mapper.Map<OrderCreateEvent>(bid);
    //        eventMessage.Quantity = auction.Quantity;

    //        auction.Status = (int)Status.Closed;
    //        bool updataResponse = await _auctionService.Update(auction);
    //        if(!updataResponse)
    //        {
    //            _logger.LogError("Auction could not updated");
    //            return BadRequest();
    //        }

    //        try
    //        {
    //            _eventBus.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, " Error Publishing Integration event : {EventId} from {AppName}", eventMessage.Id, "Sourcing");
    //            throw;
    //        }
    //        return Accepted();
    //    }
   
    //[HttpPost(Name ="TestEvent")]
    //public  ActionResult<OrderCreateEvent> TestEvent()
    //    {
    //        OrderCreateEvent eventMessage = new OrderCreateEvent();
    //        eventMessage.AuctionId = "dummy";
    //        eventMessage.ProductId = "dummy_product";
    //        eventMessage.Price = 10;
    //        eventMessage.Quantity = 10;
    //        eventMessage.SellerUserName="test@test.com";

    //        try
    //        {
    //            _eventBus.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, " Error Publishing Integration event : {EventId} from {AppName}", eventMessage.Id, "Sourcing");
    //            throw;
    //        }

    //        return Accepted(eventMessage);
    //    }
    }
}
