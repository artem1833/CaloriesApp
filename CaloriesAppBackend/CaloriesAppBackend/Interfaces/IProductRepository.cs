using CaloriesAppBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(string userId);
    }
}
