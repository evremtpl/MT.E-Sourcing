using MT.E_Sourcing.Sourcing.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Data.Interfaces
{
    public interface IBidRepository
    {

        Task SendBid(Bid bid);

        Task<List<Bid>> GetBidByAuctionId(string id);

        Task<Bid> GetWinnerBid(string id);
    }
}
