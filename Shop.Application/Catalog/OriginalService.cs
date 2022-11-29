using Microsoft.EntityFrameworkCore;
using Shop.Data.EF;
using Shop.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Catalog
{
    public class OriginalService : IOriginalService
    {
        private readonly ShopOnlineAppContext _context;
        public OriginalService(ShopOnlineAppContext context)
        { _context = context; }

        public async Task<List<OriginalVm>> GetAll()
        {
            var data = await _context.Originals.Select(x => new OriginalVm()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return data;
        }
    }
}
