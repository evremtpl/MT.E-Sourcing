using MT.E_Sourcing.WebApp.Core.Entities;
using MT.E_Sourcing.WebApp.Core.Repositories.Base;
using MT.E_Sourcing.WebApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.E_Sourcing.WebApp.Infrastructure.Repositories.Base
{
    public class UserRepository :Repository<AppUser>, IUserRepository
    {

        private readonly WebAppContext _context;

        public UserRepository(WebAppContext context) :base(context)
        {
            _context = context;
        }


        
    }
}
