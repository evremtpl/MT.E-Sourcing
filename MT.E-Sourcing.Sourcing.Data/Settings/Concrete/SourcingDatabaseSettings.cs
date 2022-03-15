using MT.E_Sourcing.Sourcing.Data.Settings.Interface;

namespace MT.E_Sourcing.Sourcing.Data.Settings.Concrete
{
    public class SourcingDatabaseSettings : ISourcingDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
