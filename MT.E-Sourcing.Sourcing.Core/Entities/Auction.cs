using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MT.E_Sourcing.Sourcing.Core.Entities
{
    public class Auction
    {
        public Auction()
        {
            IncludeSellers = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name  { get; set; }

        public string Description { get; set; }

        public string ProductId  { get; set; }

        public int Quantity { get; set; }

        public DateTime StartDate  { get; set; }
        public DateTime FinishDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public List<string> IncludeSellers { get; set; }
    }

    public enum  Status
    {
        Active=0,
        Closed=1,
        Passive=2
    }
}
