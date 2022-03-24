using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.UI.ViewModel
{
    public class AuctionViewModel
    {
  
        public string Id { get; set; }
        [Required(ErrorMessage = "Please Fill Name")]
        public string Name  { get; set; }
        [Required(ErrorMessage = "Please Fill Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Fill Product")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "Please Fill Ouantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please Fill StartDate")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please Fill FinishDate")]
        public DateTime FinishDate { get; set; }
        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public int SellerId { get; set; }
        public List<string> IncludedSellers { get; set; }
    }
}
