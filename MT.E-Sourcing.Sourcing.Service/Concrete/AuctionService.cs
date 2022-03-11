using MT.E_Sourcing.Sourcing.Core.Entities;
using MT.E_Sourcing.Sourcing.Data.Interfaces;
using MT.E_Sourcing.Sourcing.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Service.Concrete
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionService(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }
        public async Task Add(Auction auction)
        {
           await _auctionRepository.Add(auction);
        }

        public async Task<bool> Delete(string id)
        {
            return await _auctionRepository.Delete(id);
        }

        public  async Task<Auction> GetAuction(string id)
        {
            return await _auctionRepository.GetAuction(id);
        }

        public async Task<Auction> GetAuctionByName(string name)
        {
            return await _auctionRepository.GetAuctionByName(name);
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _auctionRepository.GetAuctions();
        }

        public async Task<bool> Update(Auction auction)
        {
            return await _auctionRepository.Update(auction);
        }
    }
}
