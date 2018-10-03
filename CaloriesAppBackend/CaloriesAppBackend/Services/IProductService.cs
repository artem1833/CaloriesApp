﻿using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync(string userId);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<Product> FindProductByIdAsync(int productId);

        Task<IEnumerable<ProductUserInfo>> GetProductsUserAsync(string userId);
        Task<ProductUserInfo> GetSumProductsUserAsync(string userId);
        Task AddProductUserAsync(ProductUser productUser, string userId);
        Task DeleteProductUserAsync(ProductUser productUser);
        Task<ProductUser> FindProductUserByIdAsync(int productUserId);

        Task<IEnumerable<InterpretationViewModel>> GetInterpretationAsync(int type);
    }
}
