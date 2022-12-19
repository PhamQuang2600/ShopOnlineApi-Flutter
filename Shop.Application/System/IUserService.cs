
using Shop.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.System
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);
        Task<int> Register(RegisterRequest request);
        Task<UserVm> GetById(string token);
        Task<bool> LogOut(string token);
        Task<bool> Delete(Guid id);
    }
}
