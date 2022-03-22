using System.ComponentModel.DataAnnotations;

namespace MT.E_Sourcing.UI.ViewModel
{
    public class AppUserViewModel
    {
        [Required(ErrorMessage ="User Name is required")]
        [Display(Name ="User Name")]
        public string UserName  { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password min must be 4 character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Is Buyer is required")]
        [Display(Name = "Is Buyer")]
        public bool IsBuyer { get; set; }

        [Required(ErrorMessage = "Is Seller is required")]
        [Display(Name = "Is Seller")]
        public bool IsSeller { get; set; }

        [Required(ErrorMessage = "User Type Select is required")]
        public int UserSelectTypeId { get; set; }



    }
}
