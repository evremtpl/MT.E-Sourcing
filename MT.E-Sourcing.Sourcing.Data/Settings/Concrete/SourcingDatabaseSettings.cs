using MT.E_Sourcing.Sourcing.Data.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Data.Settings.Concrete
{
    public class SourcingDatabaseSettings : ISourcingDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get ; set; }
    }
}
