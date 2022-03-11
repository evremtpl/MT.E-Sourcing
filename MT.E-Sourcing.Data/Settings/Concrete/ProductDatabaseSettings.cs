

using MT.E_Sourcing.Data.Settings.Interfaces;

namespace MT.E_Sourcing.Data.Settings.Concrete
{
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
