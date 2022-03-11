using MT.E_Sourcing.Sourcing.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Service.Interfaces
{
    public interface IAuctionService
    {

        Task<IEnumerable<Auction>> GetAuctions();

        Task<Auction> GetAuction(string id);

        Task<Auction> GetAuctionByName(string name);

        Task Add(Auction auction);
        Task<bool> Update(Auction auction);

        Task<bool> Delete(string id);
    }
}
