using MongoDB.Driver;
using MT.E_Sourcing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Data.Concrete
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct) { productCollection.InsertManyAsync(CreateProducts()); }
        }

        private static IEnumerable<Product> CreateProducts()
        {
            return new List<Product>()
            {
             new Product
             {
                 Name="Iphone 7 Plus",
                 Summary="THis is a SmartPhone",
                 Description= "SmartPhone",
                 ImageFile="Product1.png",
                 Price="840.00M",
                 Category="Smart Phone"

             },
              new Product
             {
                 Name="Iphone 8 Plus",
                 Summary="THis is a SmartPhone",
                 Description= "SmartPhone",
                 ImageFile="Product1.png",
                 Price="840.00M",
                 Category="Smart Phone"

             }
            };
        }
    }
}
