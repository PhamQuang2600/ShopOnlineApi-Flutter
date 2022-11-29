using Shop.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.System
{
    public interface ICartService
    {
        Task<List<CartVm>> GetAll(Guid uid);
        Task<int> Create(AddCartRequest request);
        Task<int> Update(UpdateCartRequest request);
        Task<int> Delete(int productID);
    }
}
