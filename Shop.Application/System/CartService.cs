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
            var cartUser = await _context.Carts.Where(x=>x.Uid== request.Uid).SingleOrDefaultAsync();

            var checkCart = await _context.Carts.Where(x => x.ProductId == request.ProductId && x.Uid == request.Uid).SingleOrDefaultAsync();
            var productExist = (from products in _context.Carts
                                where products.ProductId == request.ProductId
                                select products).Any();
            var userExist = (from users in _context.Carts
                                where users.Uid == request.Uid
                                select users).Any();

            var total = request.NumberProduct * product.Price;
            if (productExist == true && userExist)
            {
                checkCart.NumberProduct = request.NumberProduct;
                checkCart.Total += total;
                await _context.SaveChangesAsync();
                return checkCart.Id;
            }
            else if(productExist == false && userExist)
            {
                var cart = new Cart()
                {
                    CounterInCart = cartUser.CounterInCart += 1,
                    DateAddCart = DateTime.Now,
                    
                    NumberProduct = request.NumberProduct,
                    ProductId = request.ProductId,
                    Uid = request.Uid,
                    Total = total,
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart.Id;
            }
            else
            {
                var cart = new Cart()
                {
                    CounterInCart = 1,
                    DateAddCart = DateTime.Now,
                    
                    NumberProduct = request.NumberProduct,
                    ProductId = request.ProductId,
                    Uid = request.Uid,
                    Total = total,
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart.Id;
            }
        }

        public async Task<int> Delete(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null)
            {
                throw new Exception($"Can't find a product:{cartId}");
            }
            
            _context.Carts.Remove(cart);
            cart.CounterInCart -= 1;
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

        public async Task<int> Update(UpdateCartRequest request, int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            var user = await _context.Users.FindAsync(request.Uid);
            var product = await _context.Products.FindAsync(request.ProductId);

            var total = request.NumberProduct * product.Price;

            if (cart == null && user == null && product == null)
            {
                throw new Exception($"Can't find a cart: {cartId}");
            }

            cart.Uid = request.Uid;
            cart.ProductId = request.ProductId;
            cart.Id= cartId;
            
            cart.NumberProduct = request.NumberProduct;
            cart.Total = total;
            cart.FeeShipping = request.FeeShipping !=null ? request.FeeShipping : 0;
            
            return await _context.SaveChangesAsync();
        }
    }
}
