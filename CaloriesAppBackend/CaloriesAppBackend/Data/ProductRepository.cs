using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Data
{
    public class ProductRepository: EfRepository<Product>, IProductRepository
    {
        public ProductRepository(CaloriesContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Product>> GetProductsAsync(string userId)
        {
            return await db.Products.Where(x => x.UserId == null || x.UserId.Equals(userId)).ToListAsync();
        }
    }
}
