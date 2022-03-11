

using MongoDB.Driver;
using MT.E_Sourcing.Sourcing.Core.Entities;
using MT.E_Sourcing.Sourcing.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Data.Concrete
{
    public class BidRepository : IBidRepository
    {

        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }
        public async Task<List<Bid>> GetBidByAuctionId(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(m => m.AuctionId, id);
          List<Bid> bids= await  _context.Bids.Find(filter).ToListAsync();

            bids = bids.OrderByDescending(a => a.CreateDate)
                .GroupBy(a => a.SellerUserName)
                .Select(a => new Bid
                {
                    AuctionId = a.FirstOrDefault().AuctionId,
                    Price = a.FirstOrDefault().Price,
                    CreateDate = a.FirstOrDefault().CreateDate,
                    SellerUserName = a.FirstOrDefault().SellerUserName,
                    ProductId = a.FirstOrDefault().ProductId,
                    Id = a.FirstOrDefault().Id
                }).ToList();

            return bids;
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            List<Bid> bids = await GetBidByAuctionId(id);

            return bids.OrderBy(a => a.Price).FirstOrDefault();
        }

        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }
    }
}
