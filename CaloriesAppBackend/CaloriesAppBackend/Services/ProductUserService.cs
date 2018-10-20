using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using CaloriesAppBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Services
{
    public class ProductUserService: IProductUserService
    {
        private IProductUserRepository productUserRepository;
        private IInterpretationRepository interpretationRepository;

        public ProductUserService(IProductUserRepository productUserRepository, IInterpretationRepository interpretationRepository)
        {
            this.productUserRepository = productUserRepository;
            this.interpretationRepository = interpretationRepository;
        }

        public async Task<IEnumerable<ProductUserDto>> GetProductsUserAsync(string userId)
        {
            return (await productUserRepository.GetProductsUserAsync(userId)).Select(x => new ProductUserDto
            {
                Id = x.Id,
                Count = x.Count,
                Name = x.Product.Name,
                UnitOfMeasure = (interpretationRepository.FindInterpretationByType<UnitOfMeasureInterpretation>(x.Product.UnitOfMeasure)).Name,
                Weight = x.Product.Weight,
                Calorie = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Calorie),
                Protein = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Protein),
                Fat = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Fat),
                Carbohydrate = CalculateCount(x.Product.UnitOfMeasure, x.Product.Weight, x.Count, x.Product.Carbohydrate),
                GlycemicIndex = x.Product.GlycemicIndex
            });
        }

        public async Task<ProductUserDto> GetSumProductsUserAsync(string userId)
        {
            var products = await productUserRepository.GetSumProductsUserAsync(userId);

            return new ProductUserDto
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
            await productUserRepository.AddAsync(productUser);
        }

        public async Task<ProductUser> FindProductUserByIdAsync(int productUserId)
        {
            return await productUserRepository.FindProductUserByIdAsync(productUserId);
        }

        public async Task DeleteProductUserAsync(ProductUser productUser)
        {
            await productUserRepository.DeleteAsync(productUser);
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
