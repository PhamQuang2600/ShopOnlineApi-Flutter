using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Data.EF;
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

        public async Task<string> Authencate(LoginRequest request)
        {
            var users = _context.Users.SingleOrDefault(x => x.UserName == request.user);

            if (users == null)
                return("Account not exists!");

            if (users.PasswordHash != request.password)
            {
                return("Password wrong!");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, users.Email),
                new Claim(ClaimTypes.GivenName, users.Name),
                new Claim(ClaimTypes.Name, request.user)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("https://webapi.shoponlineapp.vn",
                "https://webapi.shoponlineapp.vn",
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserVm> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return null;
            }
            var userVm = new UserVm()
            {
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Address = user.Address,
                ImageUser = user.ImageUser!=null? user.ImageUser:null,
                Name= user.Name,
                Password = user.PasswordHash,
                Uid = user.Id
                
            };
            return userVm;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return false;
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return false;
            }
            user = new User()
            {
                Email = request.Email,
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
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception($"Can't find a user:{id}");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
