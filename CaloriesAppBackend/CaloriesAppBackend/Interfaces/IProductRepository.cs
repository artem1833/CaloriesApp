using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Interfaces
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(string userId);
    }
}
