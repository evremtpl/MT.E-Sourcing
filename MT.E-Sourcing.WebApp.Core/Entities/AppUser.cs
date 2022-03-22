using Microsoft.AspNetCore.Identity;


namespace MT.E_Sourcing.WebApp.Core.Entities
{
   public class AppUser : IdentityUser
    {
        public string FirstName  { get; set; }
        public string LasttName { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
    }
}
