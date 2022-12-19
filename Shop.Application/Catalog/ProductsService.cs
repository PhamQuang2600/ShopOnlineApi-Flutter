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

        public  ProductsService(ShopOnlineAppContext context)
        {
            _context = context;
            
        }
        public async Task<ProductVm> GetByID(int productID)
        {
            var product = await _context.Products.FindAsync(productID);
            var productCategory = (from a in _context.Categories
                       join b in _context.ProductCategories
                       on a.Id equals b.CategoryId
                       where b.ProductId == productID
                       select new { a.Name}
                       );
            var originalProduct = (from a in _context.Originals
                                   join b in _context.OriginalProducts
                                   on a.Id equals b.OriginalId
                                   where b.ProductId == productID
                                   select new { a.Name}
                       );

            var productViewModel = new ProductVm()
            {
                id = product.Id,
                createdDate = product.CreatedDate.ToString(),
                description = product.Description != null ? product.Description : null,


                name = product.Name,
                original = originalProduct.Select(x => x.Name).SingleOrDefault().ToString(),
                price = product.Price,

                category = productCategory.Select(x=>x.Name).SingleOrDefault().ToString(),
                imageProduct = product.ImageProduct
            };
            return productViewModel;
            
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
                    id = x.a.Id,
                    createdDate = x.a.CreatedDate.ToString(),
                    description = x.a.Description != null ? x.a.Description : null,
                    
                        
                    name = x.a.Name,
                    original = x.e.Name,
                    price = x.a.Price,
                    
                     category= x.c.Name,
                    imageProduct = x.a.ImageProduct
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
                    id = x.a.Id,
                    createdDate = x.a.CreatedDate.ToString(),
                    description = x.a.Description != null ? x.a.Description : null,


                    name = x.a.Name,
                    original = x.e.Name,
                    price = x.a.Price,

                    category = x.c.Name,
                    imageProduct = x.a.ImageProduct
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
                    id = x.a.Id,
                    createdDate = x.a.CreatedDate.ToString(),
                    description = x.a.Description != null ? x.a.Description : null,


                    name = x.a.Name,
                    original = x.e.Name,
                    price = x.a.Price,

                    category = x.c.Name,
                    imageProduct = x.a.ImageProduct
                }).ToListAsync();
            return data;
        }

        public async Task<List<ProductVm>> SearchProduct(string keyword)
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
            if (!string.IsNullOrEmpty(keyword))
                query.Where(x => x.a.Name.Contains(keyword)|| x.c.Name.Contains(keyword));
            var data = await query.OrderByDescending(x => x.a.Name).Select(x => new ProductVm()
            {
                id = x.a.Id,
                createdDate = x.a.CreatedDate.ToString(),
                description = x.a.Description != null ? x.a.Description : null,

                name = x.a.Name,
                original = x.e.Name,
                price = x.a.Price,

                category = x.c.Name,
                imageProduct = x.a.ImageProduct
            }).ToListAsync();
            return data;
        }
    }
}
