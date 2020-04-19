using CaloriesAppBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(string userId);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
