using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Services
{
    public interface IUserService
    {
        Task<UserInfoViewModel> FindUserInfoAsync(string userId);
        Task AddOrUpdateUserInfoAsync(UserInfoViewModel model, string userId);
    }
}
