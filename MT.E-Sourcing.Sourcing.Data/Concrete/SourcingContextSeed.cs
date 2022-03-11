using MongoDB.Driver;
using MT.E_Sourcing.Sourcing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MT.E_Sourcing.Sourcing.Data.Concrete
{
    public static class SourcingContextSeed
    {

        public static void SeedData(IMongoCollection<Auction> auctionCollection) //PreLoading işlemi
        {
            var exist = auctionCollection.Find(p => true).Any(); //herhangi bir eleman var mı?

            auctionCollection.InsertManyAsync(GetConfiguredAuctions());
        }

        private static IEnumerable<Auction> GetConfiguredAuctions()
        {
            return new List<Auction>()
            {
                new Auction ()
                {
                    Name="Auction 1",
                    Description="Auction Cream",
                    CreateDate=DateTime.Now,
                    StartDate=DateTime.Now,
                    FinishDate=DateTime.Now.AddDays(10),
                    ProductId="60093337093d7352d5467341",
                    IncludeSellers = new List<string>()
                    {
                        "seller@test.com",
                        "seller1@test.com",
                        "seller2@test.com"
                    },
                    Quantity=5,
                    Status=(int)Status.Active
                },
                new Auction ()
                {
                    Name="Auction 2",
                    Description="Auction Cream",
                    CreateDate=DateTime.Now,
                    StartDate=DateTime.Now,
                    FinishDate=DateTime.Now.AddDays(10),
                    ProductId="60093337093d7352d5467341",
                    IncludeSellers = new List<string>()
                    {
                        "seller@test.com",
                        "seller1@test.com",
                        "seller2@test.com"
                    },
                    Quantity=5,
                    Status=(int)Status.Active
                },
                new Auction ()
                {
                    Name="Auction 3",
                    Description="Auction Cream",
                    CreateDate=DateTime.Now,
                    StartDate=DateTime.Now,
                    FinishDate=DateTime.Now.AddDays(10),
                    ProductId="60093337093d7352d5467341",
                    IncludeSellers = new List<string>()
                    {
                        "seller@test.com",
                        "seller1@test.com",
                        "seller2@test.com"
                    },
                    Quantity=5,
                    Status=(int)Status.Active
                }

            };

        }
    }
}
