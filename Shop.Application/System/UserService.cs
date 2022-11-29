using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Data.EF;
using Shop.ViewModels.Common;
using Shop.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
            _config = config;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Account);
            if (user == null)
                return new ApiErrorResult<string>("Account not exists");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RemeberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Login fail");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("User isn't exists");
            }
            var userVm = new UserVm()
            {
                Email = user.Email,
                Dob = user.Dob,
                Account = user.UserName,
                Uid = user.Id,
                Phone = user.Phone,
                Address = user.Address,
                ImageUser = user.ImageUser,
                Name= user.Name,
                Password = user.PasswordHash

            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Account);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Account has exists!");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email has exists!");
            }
            user = new User()
            {
                Email = request.Email,
                Dob = request.Dob,
                UserName = request.Account,
                Phone = request.Phone,
                Address = request.Address,
                Name = request.Name,
                PasswordHash = request.Password
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Sign up fail!");
        }
    }
}
