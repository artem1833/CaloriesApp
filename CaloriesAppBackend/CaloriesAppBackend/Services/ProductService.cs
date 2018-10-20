using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using CaloriesAppBackend.Models.Entities;

namespace CaloriesAppBackend.Services
{
    public class ProductService: IProductService
    {
        private IProductRepository productRepository;
        private IInterpretationRepository interpretationRepository;

        public ProductService(IProductRepository productRepository, IInterpretationRepository interpretationRepository)
        {
            this.productRepository = productRepository;
            this.interpretationRepository = interpretationRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string userId)
        {
            return (await productRepository.GetProductsAsync(userId)).Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                UnitOfMeasure = (interpretationRepository.FindInterpretationByType<UnitOfMeasureInterpretation>(x.UnitOfMeasure)).Name,
                Calorie = x.Calorie,
                Protein = x.Protein,
                Fat = x.Fat,
                Carbohydrate = x.Carbohydrate,
                GlycemicIndex = x.GlycemicIndex
            });
        }


        public async Task AddProductAsync(Product product)
        {
            await productRepository.AddAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await productRepository.DeleteAsync(product);
        }
    }
}
