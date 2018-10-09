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

namespace CaloriesAppBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("products")]
        public async Task<OperationDataResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var products = await productService.GetProductsAsync(userId);
            return new OperationDataResult<IEnumerable<ProductViewModel>>(true, products);
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
        public async Task<OperationDataResult<IEnumerable<ProductUserInfo>>> GetProductUsers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var productsUser = await productService.GetProductsUserAsync(userId);
            return new OperationDataResult<IEnumerable<ProductUserInfo>>(true, productsUser);
        }

        [HttpGet]
        [Route("sumProductsUser")]
        public async Task<OperationDataResult<ProductUserInfo>> GetSumProductUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var productsUser = await productService.GetSumProductsUserAsync(userId);
            return new OperationDataResult<ProductUserInfo>(true, productsUser);
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

            await productService.AddProductUserAsync(productUser, userId);

            return new OperationResult(true, "Продукт добавлен");

        }

        [HttpDelete]
        [Route("delete-productUser/{id}")]
        public async Task<OperationResult> DeleteProductUser([FromRoute] int id)
        {
            var productUser = await productService.FindProductUserByIdAsync(id);
            if (productUser == null)
            {
                return new OperationResult(false, "Продукт не найден");
            }

            await productService.DeleteProductUserAsync(productUser);

            return new OperationResult(true, "Продукт удален");
        }

        [HttpGet]
        [Route("interpretation/{type}")]
        public async Task<IEnumerable<InterpretationViewModel>> GetInterpretation(int type)
        {
            return await productService.GetInterpretationAsync(type);
        }
    }
}