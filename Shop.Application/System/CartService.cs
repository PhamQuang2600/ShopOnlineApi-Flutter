using Microsoft.EntityFrameworkCore;
using Shop.Data.EF;
using Shop.ViewModels.Catalog;
using Shop.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop.Application.System
{
    public class CartService : ICartService
    {
        private readonly ShopOnlineAppContext _context;

        public CartService(ShopOnlineAppContext context)
        {
            _context = context;
        }

        public async Task<int> Create(AddCartRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);

            var total = request.NumberProduct * product.Price + request.FeeShipping;
            var cart = new Cart()
            {
                CounterInCart = 1,
                DateAddCart= DateTime.Now,
                FeeShipping = 2,
                NumberProduct = 1,
                ProductId= request.ProductId,
                Uid =request.Uid,
                Total = total,
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart.ProductId;
        }

        public async Task<int> Delete(int productID)
        {
            var cart = await _context.Carts.FindAsync(productID);
            if (cart == null)
            {
                throw new Exception($"Can't find a product:{productID}");
            }
            
            _context.Carts.Remove(cart);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<CartVm>> GetAll(Guid uid)
        {
            var query = from a in _context.Carts
                       where a.Uid ==uid select new { a };
            var data = await query.OrderByDescending(x=>x.a.NumberProduct)
                .Select(x => new CartVm()
                {
                    Uid = x.a.Uid,
                    ProductId =x.a.ProductId,
                    CounterInCart= x.a.CounterInCart,
                    FeeShipping = x.a.FeeShipping,
                    NumberProduct= x.a.NumberProduct,
                    Total = x.a.Total,
                }).ToListAsync();
            return data;
        }

        public async Task<int> Update(UpdateCartRequest request)
        {
            var cart = await _context.Carts.FindAsync(request.ProductId);
            var product = await _context.Products.FindAsync(request.ProductId);

            var total = request.NumberProduct * product.Price + request.FeeShipping;

            if (cart == null)
            {
                throw new Exception($"Can't find a product: {request.ProductId}");
            }

            
            cart.NumberProduct = request.NumberProduct;
            cart.Total = total;
            cart.FeeShipping = request.FeeShipping;
            
            return await _context.SaveChangesAsync();
        }
    }
}
