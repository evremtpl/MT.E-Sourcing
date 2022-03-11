using MongoDB.Driver;
using MT.E_Sourcing.Sourcing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Data.Interfaces
{
  public  interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
