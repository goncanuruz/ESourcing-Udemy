using E_SourcingProducts.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_SourcingProducts.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Product> GetConfigureProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name="LG G7 ThinQ",
                    Category="Smart Phone",
                    Description="One thing we always liked in the LG headliners is the shock resistance that comes in addition to the water-proofing. And the G7 is MIL-STD-810G compliant for shock endurance, " +
                    "just like previous G and V models, but we'll get to that in a bit. First, here's a primer on the LG G7 key specs.",
                    ImageFile="lg-4.png",
                    Price=380.00M,
                    Summary="LG is finally releasing a G-series phone with the latest Snapdragon - yay! Regardless whether we have some supply chain optimization or LG's product planning to thank, the fact of the matter is the G7 ThinQ is now available internationally and it's a proper high-end device."
                },
                new Product
                {
                    Name="Xaomi RedMi Note 8",
                    Category="Smart Phone",
                    Description="With the impressive MediaTek Helio G85 gaming processor, the device offers superior performance. The octa-core CPU operates at up to 2.0GHz, increasing app response speed and further enhancing your game experience",
                    ImageFile="xiaomi-4.png",
                    Price=180.00M,
                    Summary="No need to step back, capture the whole scene standing where you are."
                },
                new Product
                {
                    Name="Nokia 3310",
                    Category="Phone",
                    Description="Best phone ever",
                    ImageFile="nokia-4.png",
                    Price=180.00M,
                    Summary="No need to protection."
                }
            };
        }
    }
}
