using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_SourcingProducts.Settings
{
   public interface IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
