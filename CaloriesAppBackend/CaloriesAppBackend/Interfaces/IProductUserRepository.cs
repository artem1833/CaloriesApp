using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Interfaces
{
    public interface IProductUserRepository : IRepository<ProductUser>
    {
        Task<IEnumerable<ProductUser>> GetProductsAsync(string userId);
        Task<ProductUser> FindProductUserByIdAsync(int productUserId);
        Task<IEnumerable<ProductUser>> GetProductsUserAsync(string userId);
        Task<IEnumerable<ProductUser>> GetSumProductsUserAsync(string userId);
    }
}
