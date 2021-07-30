using E_SourcingProducts.Entities;
using MongoDB.Driver;
namespace E_SourcingProducts.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
