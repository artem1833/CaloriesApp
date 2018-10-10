using AutoMapper;
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
        private CaloriesContext db;

        public UserService(CaloriesContext context)
        {
            db = context;
        }

        public async Task<UserInfoDto> FindUserInfoAsync(string userId)
        {
            var userInfo = await db.UserInfo.FirstOrDefaultAsync(x => x.Id.Equals(userId));
            return Mapper.Map<UserInfoDto>(userInfo);

        }

        public async Task AddOrUpdateUserInfoAsync(UserInfoDto model, string userId)
        {
            var userInfo = await FindUserInfoAsync(userId);

            if (userInfo == null)
            {
                var newUserInfo = Mapper.Map<UserInfo>(model);
                newUserInfo.Id = userId;
                await db.UserInfo.AddAsync(newUserInfo);
            }
            else
            {
                userInfo.Height = model.Height;
                userInfo.Weight = model.Weight;
                userInfo.Age = model.Age;
                userInfo.Gender = model.Gender;
                userInfo.PhysicalActivity = model.PhysicalActivity;
                userInfo.Purpose = model.Purpose;
                db.Entry(userInfo).State = EntityState.Modified;
            }
            await db.SaveChangesAsync();

        }

    }
}
