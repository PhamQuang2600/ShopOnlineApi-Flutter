using Microsoft.EntityFrameworkCore;
using Shop.Data.EF;
using Shop.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shop.Application.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly ShopOnlineAppContext _context;
        public CategoryService(ShopOnlineAppContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryVm>> GetAll()
        {
            var data = await _context.Categories.Select(x => new CategoryVm()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return data;
        }
    }
}
