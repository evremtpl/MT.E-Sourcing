using System;


namespace MT.E_Sourcing.UI.ViewModel
{
    public class BidViewModel
    {

        public string Id { get; set; }
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public string Price { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
