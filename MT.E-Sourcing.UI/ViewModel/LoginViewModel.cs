using System.ComponentModel.DataAnnotations;

namespace MT.E_Sourcing.UI.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Password min must be 4 character")]
        public string Password { get; set; }
    }
}
