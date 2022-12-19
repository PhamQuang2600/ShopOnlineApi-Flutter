using Microsoft.EntityFrameworkCore;
using Shop.Data.EF;
using Shop.ViewModels.Catalog;
using Shop.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
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
            var cartUser = await _context.Carts.Where(x=>x.Uid== request.Uid && x.ProductId == request.ProductId).SingleOrDefaultAsync();

            var checkCart = await _context.Carts.Where(x => x.ProductId == request.ProductId && x.Uid == request.Uid).SingleOrDefaultAsync();
            var productExist = (from products in _context.Carts
                                where products.ProductId == request.ProductId
                                select products).Any();
            var userExist = (from users in _context.Carts
                                where users.Uid == request.Uid
                                select users).Any();
            if(cartUser ==null)
            {
                var cart = new Cart()
                {
                    CounterInCart = 1,
                    ProductImage = product.ImageProduct,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    DateAddCart = DateTime.Now,
                    NumberProduct = 1,
                    ProductId = request.ProductId,
                    Uid = request.Uid,
                    Total = product.Price,
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart.Id;
            }

            var total = cartUser.NumberProduct * product.Price;
            if (productExist == true && userExist == true)
            {

                checkCart.NumberProduct = cartUser.NumberProduct+1;
                checkCart.Total += total;
                await _context.SaveChangesAsync();
                return checkCart.Id;
            }
            else if(productExist == false && userExist == true)
            {
                var cart = new Cart()
                {
                    CounterInCart = cartUser.CounterInCart += 1,
                    ProductImage = product.ImageProduct,
                    ProductName = product.Name,
                    ProductPrice = product.Price,

                    NumberProduct = 1,
                    ProductId = request.ProductId,
                    Uid = request.Uid,
                    Total = cartUser.Total+total,
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
                    ProductImage = product.ImageProduct,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    DateAddCart = DateTime.Now,
                    NumberProduct = 1,
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
            cart.TotalAll -= cart.Total;
            _context.Carts.Remove(cart);
            cart.CounterInCart -= 1;
            
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CartVm>> GetAll(Guid uid)
        {
            var query = from a in _context.Carts
                        join b in _context.Products
                        on a.ProductId equals b.Id
                       where a.Uid ==uid select new { a,b };
            
            var data = await query.OrderByDescending(x=>x.a.NumberProduct)
                .Select(x => new CartVm()
                {
                    Id = x.a.Id,
                    Uid = x.a.Uid,
                    ProductId =x.b.Id,
                    FeeShipping = x.a.FeeShipping,
                    NumberProduct= x.a.NumberProduct,
                    ProductImage = x.b.ImageProduct,
                    ProductName = x.b.Name,
                    ProductPrice = x.b.Price,
                    Total = x.a.Total,
                }).ToListAsync();
           var totalAll= data.Sum(x => x.Total);
            var counter = data.Count;
            foreach(var item in data )
            {
                item.TotalAll = totalAll;
                item.CounterInCart = counter;
            }
            await _context.SaveChangesAsync();
            if (data.Count == 0)
            {
                return null;
            }
            return data;
        }

        public async Task<CartVm> GetById(int cartId)
        {
            var data = await _context.Carts.SingleOrDefaultAsync(x=>x.Id == cartId);
            
            if (data != null)
            {
                var product = await _context.Products.FindAsync(data.ProductId);
                var cart = new CartVm()
                {
                    Uid = data.Uid,
                    ProductId = product.Id,
                    Id = cartId,
                    ProductImage = product.ImageProduct,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    CounterInCart = data.CounterInCart,
                    FeeShipping = data.FeeShipping,
                    NumberProduct = data.NumberProduct,
                    Total = data.Total
                };
                return cart;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Update(UpdateCartRequest request)
        {
            var cart = await _context.Carts.FindAsync(request.CartId);
            var user = await _context.Users.FindAsync(request.Uid);
            var product = await _context.Products.FindAsync(request.ProductId);
            
            

            var total = request.NumberProduct * product.Price;

            if (cart == null && user == null && product == null)
            {
                throw new Exception($"Can't find a cart: {request.CartId}");
            }

            cart.Uid = request.Uid;
            cart.ProductId = request.ProductId;
            cart.Id= request.CartId;
            
            cart.NumberProduct = request.NumberProduct;
            cart.Total = total;
            cart.TotalAll+= total - cart.ProductPrice;
            cart.FeeShipping = request.FeeShipping !=null ? request.FeeShipping : 0;
            
            return await _context.SaveChangesAsync();
        }
    }
}
