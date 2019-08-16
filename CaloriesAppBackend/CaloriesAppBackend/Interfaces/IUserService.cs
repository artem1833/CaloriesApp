using CaloriesAppBackend.Models;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Services
{
    public interface IUserService
    {
        Task<UserInfo> FindUserInfoAsync(string userId);
        Task AddOrUpdateUserInfoAsync(UserInfoDto model, string userId);
    }
}
