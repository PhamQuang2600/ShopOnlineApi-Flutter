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
    public class ProductsService : IProductsService
    {
        private readonly ShopOnlineAppContext _context;

        public ProductsService(ShopOnlineAppContext context)
        {
            _context = context;
            
        }
        public async Task<ProductVm> GetByID(int productID)
        {
            var query = from a in _context.Products
                        join b in _context.ProductCategories on a.Id equals b.ProductId into ci
                        from b in ci.DefaultIfEmpty()
                        join c in _context.Categories on b.CategoryId equals c.Id into dci
                        from c in dci.DefaultIfEmpty()
                        join d in _context.OriginalProducts on a.Id equals d.ProductId into di
                        from d in di.DefaultIfEmpty()
                        join e in _context.Originals on d.OriginalId equals e.Id into de
                        from e in de.DefaultIfEmpty()
                        where b.ProductId == productID
                        select new { a, b, c, d, e };
        
            var data = query.Where(x=>x.a.Id == productID).Select(x => new ProductVm()
                {
                    ID = x.a.Id,
                    CreatedDate = x.a.CreatedDate,
                    Description = x.a.Description != null ? x.a.Description : null,


                    Name = x.a.Name,
                    Original = x.e.Name,
                    Price = x.a.Price,

                    Category = x.c.Name,
                    ImageProduct = x.a.ImageProduct
                });
                return (ProductVm)data;
            
        }

        public async Task<List<ProductVm>> GetFeatureProduct(int take)
        {
            var query = from a in _context.Products
                        join b in _context.ProductCategories on a.Id equals b.ProductId into ci
                        from b in ci.DefaultIfEmpty()
                        join c in _context.Categories on b.CategoryId equals c.Id into dci
                        from c in dci.DefaultIfEmpty()
                        join d in _context.OriginalProducts on a.Id equals d.ProductId into di
                        from d in di.DefaultIfEmpty()
                        join e in _context.Originals on d.OriginalId equals e.Id into de
                        from e in de.DefaultIfEmpty()


                        select new {a,b,c,d,e};


            var data = await query.OrderByDescending(x => x.a.Name).Take(take)
                .Select(x => new ProductVm()
                {
                    ID = x.a.Id,
                    CreatedDate = x.a.CreatedDate,
                    Description = x.a.Description != null ? x.a.Description : null,
                    
                        
                    Name = x.a.Name,
                    Original = x.e.Name,
                    Price = x.a.Price,
                    
                     Category= x.c.Name,
                    ImageProduct = x.a.ImageProduct
                }).ToListAsync();
            return data;
        }

        public async Task<List<ProductVm>> GetLatestProduct(int take)
        {
            var query = from a in _context.Products
                        join b in _context.ProductCategories on a.Id equals b.ProductId into ci
                        from b in ci.DefaultIfEmpty()
                        join c in _context.Categories on b.CategoryId equals c.Id into dci
                        from c in dci.DefaultIfEmpty()
                        join d in _context.OriginalProducts on a.Id equals d.ProductId into di
                        from d in di.DefaultIfEmpty()
                        join e in _context.Originals on d.OriginalId equals e.Id into de
                        from e in de.DefaultIfEmpty()

                        select new { a, b, c, d, e };


            var data = await query.OrderByDescending(x => x.a.CreatedDate).Take(take)
                .Select(x => new ProductVm()
                {
                    ID = x.a.Id,
                    CreatedDate = x.a.CreatedDate,
                    Description = x.a.Description != null ? x.a.Description : null,


                    Name = x.a.Name,
                    Original = x.e.Name,
                    Price = x.a.Price,

                    Category = x.c.Name,
                    ImageProduct = x.a.ImageProduct
                }).ToListAsync();
            return data;
        }

        public async Task<List<ProductVm>> GetSameProduct(int productID, int take)
        {
            var query = from a in _context.Products
                        join b in _context.ProductCategories on a.Id equals b.ProductId into ci
                        from b in ci.DefaultIfEmpty()
                        join c in _context.Categories on b.CategoryId equals c.Id into dci
                        from c in dci.DefaultIfEmpty()
                        join d in _context.OriginalProducts on a.Id equals d.ProductId into di
                        from d in di.DefaultIfEmpty()
                        join e in _context.Originals on d.OriginalId equals e.Id into de
                        from e in de.DefaultIfEmpty()
                        where b.CategoryId == c.Id
                        select new { a, b, c, d, e };


            var data = await query.OrderByDescending(x => x.a.Id != productID).Take(take)
                .Select(x => new ProductVm()
                {
                    ID = x.a.Id,
                    CreatedDate = x.a.CreatedDate,
                    Description = x.a.Description != null ? x.a.Description : null,


                    Name = x.a.Name,
                    Original = x.e.Name,
                    Price = x.a.Price,

                    Category = x.c.Name,
                    ImageProduct = x.a.ImageProduct
                }).ToListAsync();
            return data;
        }
    }
}
