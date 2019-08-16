using System.Collections.Generic;
using System.Threading.Tasks;
using CaloriesAppBackend.Models;

namespace CaloriesAppBackend.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(string userId);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
