using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(string userId);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<Product> FindProductByIdAsync(int productId);

        Task<IEnumerable<ProductUserDto>> GetProductsUserAsync(string userId);
        Task<ProductUserDto> GetSumProductsUserAsync(string userId);
        Task AddProductUserAsync(ProductUser productUser, string userId);
        Task DeleteProductUserAsync(ProductUser productUser);
        Task<ProductUser> FindProductUserByIdAsync(int productUserId);

        Task<IEnumerable<InterpretationDto>> GetInterpretationAsync<T>() where T : Interpretation;
    }
}
