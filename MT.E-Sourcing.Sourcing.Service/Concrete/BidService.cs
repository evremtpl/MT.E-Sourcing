using MT.E_Sourcing.Sourcing.Core.Entities;
using MT.E_Sourcing.Sourcing.Data.Interfaces;
using MT.E_Sourcing.Sourcing.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Service.Concrete
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;

        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        public async Task<List<Bid>> GetBidByAuctionId(string id)
        {
            return await _bidRepository.GetBidByAuctionId(id);
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            return await _bidRepository.GetWinnerBid(id);
        }

        public async Task SendBid(Bid bid)
        {
            await _bidRepository.SendBid(bid);
        }
    }
}
