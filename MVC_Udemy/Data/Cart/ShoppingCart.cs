using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCardItem> ShoppingCardItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Configuration of session for the car
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession sesion = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<AppDbContext>();

            string cartId = sesion.GetString("CartId") ?? Guid.NewGuid().ToString();

            sesion.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCardId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCardItem()
                {
                    ShoppingCardId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.ShoppingCardItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCardId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;
                else 
                    _context.ShoppingCardItems.Remove(shoppingCartItem);
            }
            _context.SaveChanges();
        }

        public List<ShoppingCardItem> GetShoppingCartItems()
        {
            return ShoppingCardItems ?? (ShoppingCardItems = _context.ShoppingCardItems
                .Where(n => n.ShoppingCardId == ShoppingCartId)
                .Include(n => n.Movie)
                .ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();

            return total;
        }

    }
}
