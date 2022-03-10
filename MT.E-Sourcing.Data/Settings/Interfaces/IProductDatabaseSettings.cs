
namespace MT.E_Sourcing.Data.Settings.Interfaces

{
   public interface IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
