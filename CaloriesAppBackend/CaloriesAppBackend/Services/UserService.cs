using AutoMapper;
using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<UserInfo> userRepository;

        public UserService(IRepository<UserInfo> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserInfo> FindUserInfoAsync(string userId)
        {
            return await userRepository.GetByIdAsync(userId);
        }

        public async Task AddOrUpdateUserInfoAsync(UserInfoDto model, string userId)
        {
            var userInfo = await FindUserInfoAsync(userId);

            if (userInfo == null)
            {
                var newUserInfo = Mapper.Map<UserInfo>(model);
                newUserInfo.Id = userId;
                await userRepository.AddAsync(newUserInfo);
            }
            else
            {
                userInfo.Height = model.Height;
                userInfo.Weight = model.Weight;
                userInfo.Age = model.Age;
                userInfo.Gender = model.Gender;
                userInfo.PhysicalActivity = model.PhysicalActivity;
                userInfo.Purpose = model.Purpose;
                await userRepository.UpdateAsync(userInfo);
            }
        }
    }
}
