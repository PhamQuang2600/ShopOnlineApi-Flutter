
using Shop.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Catalog
{
    public interface ICategoryService
    {
        Task<List<CategoryVm>> GetAll();
    }
}
