using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Sourcing.Data.Settings.Interface
{
    public interface ISourcingDatabaseSettings
    {
       public string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
