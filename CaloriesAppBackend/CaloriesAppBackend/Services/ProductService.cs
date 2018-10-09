using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CaloriesAppBackend.Services
{
    public class ProductService: IProductService
    {
        private CaloriesContext db;

        public ProductService(CaloriesContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(string userId)
        {
            return await db.Products.Where(x => x.UserId == null || x.UserId.Equals(userId)).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                UnitOfMeasure = db.Interpretations.FirstOrDefault(y => y.Type == 1 && y.SubType == x.UnitOfMeasure).Name,
                Calorie = x.Calorie,
                Protein = x.Protein,
                Fat = x.Fat,
                Carbohydrate = x.Carbohydrate,
                GlycemicIndex = x.GlycemicIndex
            }).ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }

        public async Task<Product> FindProductByIdAsync(int productId)
        {
            return await db.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<ProductUserViewModel>> GetProductsUserAsync(string userId)
        {
            return await db.ProductUsers.Include(x => x.Product).Where(x => x.UserId.Equals(userId)).Select(x => new ProductUserViewModel
            {
                Id = x.Id,
                Count = x.Count,
                Name = x.Product.Name,
                UnitOfMeasure = db.Interpretations.FirstOrDefault(y => y.Type == 1 && y.SubType == x.Product.UnitOfMeasure).Name,
                Weight = x.Product.Weight,
                Calorie = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Calorie),
                Protein = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Protein),
                Fat = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Fat),
                Carbohydrate = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Carbohydrate),
                GlycemicIndex = x.Product.GlycemicIndex
            }).ToListAsync();
        }

        public async Task<ProductUserViewModel> GetSumProductsUserAsync(string userId)
        {
            var products = await db.ProductUsers.Include(x => x.Product).Where(x => x.UserId.Equals(userId)).ToListAsync();
            
            return new ProductUserViewModel
            {
                Count = products.Sum(x => CalculateWeight(x.Product.UnitOfMeasure, x.Product.Weight, x.Count)),
                Calorie = products.Sum(x => CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Calorie)),
                Protein = products.Sum(x => CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Protein)),
                Fat = products.Sum(x => CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Fat)),
                Carbohydrate = products.Sum(x => CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Carbohydrate))
            };
        }

        public async Task AddProductUserAsync(ProductUser productUser, string userId)
        {
            productUser.UserId = userId;
            db.ProductUsers.Add(productUser);
            await db.SaveChangesAsync();
        }

        public async Task<ProductUser> FindProductUserByIdAsync(int productUserId)
        {
            return await db.ProductUsers.FindAsync(productUserId);
        }

        public async Task DeleteProductUserAsync(ProductUser productUser)
        {
            db.ProductUsers.Remove(productUser);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<InterpretationViewModel>> GetInterpretationAsync(int type)
        {
            return await db.Interpretations.Where(x => x.Type == type)
                .Select(x => new InterpretationViewModel
                {
                    Name = x.Name,
                    Type = x.SubType
                })
                .ToListAsync();
        }

        private int CalculateCount(int unitOfMeasure, int weight, int count, int value)
        {
            switch (unitOfMeasure)
            {
                case 1:
                    int test = count * value / weight;
                    return count * value / weight;
                case 2:
                    return count * value;
                default: return 0;
            }
        }

        private int CalculateWeight(int unitOfMeasure, int weight, int count)
        {
            switch (unitOfMeasure)
            {
                case 1:
                    return count;
                case 2:
                    return count * weight;
                default: return 0;
            }
        }
    }
}
