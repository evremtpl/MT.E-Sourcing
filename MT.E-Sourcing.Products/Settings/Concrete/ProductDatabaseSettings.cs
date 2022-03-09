using MT.E_Sourcing.Products.Settings.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Products.Settings.Concrete
{
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
