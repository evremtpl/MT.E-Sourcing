using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MT.E_Sourcing.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT.E_Sourcing.WebApp.Infrastructure.Data
{
    public class WebAppContextSeed
    {

        public static async Task SeedAsync(WebAppContext context,ILoggerFactory loggerFactory, int? retry=0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                context.Database.Migrate();

                if(!context.AppUsers.Any())
                {
                    context.AppUsers.AddRange(GetPreconfiguredAppUsers());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if(retryForAvailability<50)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<WebAppContextSeed>();
                    log.LogError(ex.Message);
                    Thread.Sleep(2000);

                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
                
            }
        }

        private static IEnumerable<AppUser> GetPreconfiguredAppUsers()
        {
            return new List<AppUser>() {
            new AppUser
            {
                FirstName="Bahar",
                LasttName="Bahar",
                IsBuyer=true,
                IsSeller=false
            }

            };
        }
    }
}
