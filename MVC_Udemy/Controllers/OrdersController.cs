using Microsoft.AspNetCore.Mvc;
using MVC_Udemy.Data.Cart;
using MVC_Udemy.Data.Services;
using MVC_Udemy.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _service;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IMoviesService service, ShoppingCart shoppingCart)
        {
            _service = service;
            _shoppingCart = shoppingCart;
        }

        public IActionResult ShoppingCart()
        { 
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCardItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            
            return View(response );
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _service.GetMovieByIdAsync(id);

            if(item != null)
                _shoppingCart.AddItemCart(item);

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _service.GetMovieByIdAsync(id);

            if (item != null)
                _shoppingCart.RemoveItemCart(item);

            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
