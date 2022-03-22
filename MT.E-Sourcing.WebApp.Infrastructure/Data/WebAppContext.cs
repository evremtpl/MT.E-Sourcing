
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MT.E_Sourcing.WebApp.Core.Entities;

namespace MT.E_Sourcing.WebApp.Infrastructure.Data
{
   public class WebAppContext : IdentityDbContext<AppUser>
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers  { get; set; }
    }
}
