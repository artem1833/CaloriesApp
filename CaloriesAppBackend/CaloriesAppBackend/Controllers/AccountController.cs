using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CaloriesAppBackend.Models.Api;
using CaloriesAppBackend.Services;

namespace CaloriesAppBackend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private IUserService userService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IUserService userService
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userService = userService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<OperationDataResult<TokenResponseDto>> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return new OperationDataResult<TokenResponseDto>(false, "Неверный логин или пароль");
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return new OperationDataResult<TokenResponseDto>(true, "Успешно авторизированы!", GenerateJwtToken(model.Email, appUser));
            }
            return new OperationDataResult<TokenResponseDto>(false, "Неверный логин или пароль");
        }

        [HttpPost]
        [Route("register")]
        public async Task<OperationDataResult<TokenResponseDto>> Register(RegisterDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null) return new OperationDataResult<TokenResponseDto>(false,"Email уже существует");

            user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return new OperationDataResult<TokenResponseDto>(true, "Успешно авторизированы!", GenerateJwtToken(model.Email, user));
            }

            return new OperationDataResult<TokenResponseDto>(false, "Неверный логин или пароль");
        }

        [HttpGet]
        [Route("user-info")]
        public async Task<UserInfoDto> GetUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userInfo = await userService.FindUserInfoAsync(userId);
            return userInfo;
        }

        [HttpPost]
        [Route("user-info")]
        public async Task<object> UpdateUserInfo(UserInfoDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await userService.AddOrUpdateUserInfoAsync(model, userId);
            return new OperationResult(true,"Изменения сохранены");
        }

        [HttpGet]
        [Route("user-calories-recommended")]
        public async Task<OperationDataResult<CaloriesRecommendedDto>> GetUserCaloriesRecommended()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userInfo = await userService.FindUserInfoAsync(userId);
            var model = Mapper.Map<CaloriesRecommendedDto>(userInfo);
            return new OperationDataResult<CaloriesRecommendedDto>(true, model);
        }

        [NonAction]
        private TokenResponseDto GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["Tokens:ExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["Tokens:Issuer"],
                configuration["Tokens:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseDto()
            {
                token = encodedToken,
                expiration = Convert.ToInt32(configuration["Tokens:ExpireDays"])
            };

        }
    }
}
