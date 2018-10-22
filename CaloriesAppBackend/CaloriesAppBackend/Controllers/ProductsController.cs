using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaloriesAppBackend.Models;
using System.Security.Claims;
using CaloriesAppBackend.Models.Api;
using CaloriesAppBackend.Services;
using Microsoft.AspNetCore.Identity;
using CaloriesAppBackend.Models.Entities;
using CaloriesAppBackend.Interfaces;

namespace CaloriesAppBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IProductUserService productUserService;
        private readonly IInterpretationRepository interpretationRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(IProductService productService, IProductUserService productUserService, UserManager<ApplicationUser> userManager, IInterpretationRepository interpretationRepository)
        {
            this.productService = productService;
            this.productUserService = productUserService;
            this.userManager = userManager;
            this.interpretationRepository = interpretationRepository;
        }

        [HttpGet]
        [Route("products")]
        public async Task<OperationDataResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var products = await productService.GetProductsAsync(userId);
            return new OperationDataResult<IEnumerable<ProductDto>>(true, products);
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<OperationResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new OperationResult(false, string.Join(" ", allErrors.Select(x => x.ErrorMessage)));
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            product.UserId = userId;
            await productService.AddProductAsync(product);
            return new OperationResult(true, "Продукт добавлен");
        }

        [HttpGet]
        [Route("productsUser")]
        public async Task<OperationDataResult<IEnumerable<ProductUserDto>>> GetProductUsers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var productsUser = await productUserService.GetProductsUserAsync(userId);
            return new OperationDataResult<IEnumerable<ProductUserDto>>(true, productsUser);
        }

        [HttpGet]
        [Route("sumProductsUser")]
        public async Task<OperationDataResult<ProductUserDto>> GetSumProductUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var productsUser = await productUserService.GetSumProductsUserAsync(userId);
            return new OperationDataResult<ProductUserDto>(true, productsUser);
        }
    
        [HttpPost]
        [Route("add-productUser")]
        public async Task<OperationResult> PostProductUser([FromBody] ProductUser productUser)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new OperationResult(false, string.Join(" ", allErrors.Select(x => x.ErrorMessage)));
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new OperationResult(false, "Пользователь не найден. Авторизируйтесь!");
            }

            await productUserService.AddProductUserAsync(productUser, userId);

            return new OperationResult(true, "Продукт добавлен");

        }

        [HttpDelete]
        [Route("delete-productUser/{id}")]
        public async Task<OperationResult> DeleteProductUser([FromRoute] int id)
        {
            var productUser = await productUserService.FindProductUserByIdAsync(id);
            if (productUser == null)
            {
                return new OperationResult(false, "Продукт не найден");
            }

            await productUserService.DeleteProductUserAsync(productUser);

            return new OperationResult(true, "Продукт удален");
        }

        [HttpGet]
        [Route("interpretation/{type}")]
        public async Task<OperationDataResult<IEnumerable<InterpretationDto>>> GetInterpretation(string type)
        {
            switch (type)
            {
                case "gender":
                    return new OperationDataResult<IEnumerable<InterpretationDto>>(true, await interpretationRepository.GetInterpretationAsync<GenderInterpretation>());
                case "purpose":
                    return new OperationDataResult<IEnumerable<InterpretationDto>>(true, await interpretationRepository.GetInterpretationAsync<PurposeInterpretation>());
                case "activity":
                    return new OperationDataResult<IEnumerable<InterpretationDto>>(true, await interpretationRepository.GetInterpretationAsync<PhysicalActivityInterpretation>());
                case "unitOfMeasure":
                    return new OperationDataResult<IEnumerable<InterpretationDto>>(true, await interpretationRepository.GetInterpretationAsync<UnitOfMeasureInterpretation>());
                default:
                    return new OperationDataResult<IEnumerable<InterpretationDto>>(false,"Error Interpretation");
            }
        }
    }
}