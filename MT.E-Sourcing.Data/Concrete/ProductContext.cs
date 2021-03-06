using MongoDB.Driver;
using MT.E_Sourcing.Core;
using MT.E_Sourcing.Data.Interfaces;
using MT.E_Sourcing.Data.Settings.Interfaces;

namespace MT.E_Sourcing.Data.Concrete
{
    public class ProductContext : IProductContext
    {
        
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Products = database.GetCollection<Product>(settings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
