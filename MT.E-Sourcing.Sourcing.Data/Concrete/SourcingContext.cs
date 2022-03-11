using MongoDB.Driver;
using MT.E_Sourcing.Sourcing.Core.Entities;
using MT.E_Sourcing.Sourcing.Data.Interfaces;
using MT.E_Sourcing.Sourcing.Data.Settings.Interface;

namespace MT.E_Sourcing.Sourcing.Data.Concrete
{
    public class SourcingContext : ISourcingContext
    {

        public SourcingContext(ISourcingDatabaseSettings settings) //DI
        {
            var mongoClient = new MongoClient(settings.ConnectionString); //context nesnesine başvurulduğunda mongoclient oluşturuldu.
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));

            SourcingContextSeed.SeedData(Auctions);
        }
        public IMongoCollection<Auction> Auctions { get; }

        public IMongoCollection<Bid> Bids { get; }
    }
}
