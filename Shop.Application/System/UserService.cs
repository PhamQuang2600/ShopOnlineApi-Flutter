using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ShopOnlineAppContext _context;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, ShopOnlineAppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _config = config;
            _context = context;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == request.UserName);

            if (user == null)
                return new ApiErrorResult<string>("Account not exists");

            if (user.PasswordHash != request.Password)
            {
                return new ApiErrorResult<string>("Login fail");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("https://webapi.shoponlineapp.vn",
                "https://webapi.shoponlineapp.vn",
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
                return new ApiErrorResult<UserVm>("User not exists");
            }
            var userVm = new UserVm()
            {
                Email = user.Email,
                Dob = user.Dob,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Address = user.Address,
                ImageUser = user.ImageUser,
                Name= user.Name,
                Password = user.PasswordHash,
                Uid = user.Id
                
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
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
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Address = request.Address,
                Name = request.Name,
                PasswordHash = request.Password
                
            };
            var result = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            if (result != null)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Sign up fail!");
        }

        public async Task<int> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception($"Can't find a user:{id}");
            }

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}
