using CaloriesAppBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Interfaces
{
    public interface IProductUserService
    {
        Task<IEnumerable<ProductUserDto>> GetProductsUserAsync(string userId);
        Task<ProductUserDto> GetSumProductsUserAsync(string userId);
        Task AddProductUserAsync(ProductUser productUser, string userId);
        Task DeleteProductUserAsync(ProductUser productUser);
        Task<ProductUser> FindProductUserByIdAsync(int productUserId);
    }
}
