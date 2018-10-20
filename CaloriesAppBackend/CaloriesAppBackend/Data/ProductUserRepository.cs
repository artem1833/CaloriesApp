using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Data
{
    public class ProductUserRepository : EfRepository<ProductUser>, IProductUserRepository
    {
        public ProductUserRepository(CaloriesContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductUser>> GetProductsAsync(string userId)
        {
            return await db.ProductUsers.Where(x => x.UserId == null || x.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<ProductUser> FindProductUserByIdAsync(int productUserId)
        {
            return await db.ProductUsers.FindAsync(productUserId);
        }

        public async Task<IEnumerable<ProductUser>> GetProductsUserAsync(string userId)
        {
            return await db.ProductUsers.Include(x => x.Product).Where(x => x.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<IEnumerable<ProductUser>> GetSumProductsUserAsync(string userId)
        {
            return await db.ProductUsers.Include(x => x.Product).Where(x => x.UserId.Equals(userId)).ToListAsync();
        }
    }
}
