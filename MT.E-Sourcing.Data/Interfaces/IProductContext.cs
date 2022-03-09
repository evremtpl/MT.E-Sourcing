using MongoDB.Driver;
using MT.E_Sourcing.Core;

namespace MT.E_Sourcing.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get;}
    }
}
