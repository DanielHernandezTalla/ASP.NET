using Microsoft.AspNetCore.Mvc;
using MVC_Udemy.Data.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.ViewComponents
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart _service;

        public ShoppingCartSummary( ShoppingCart service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            var items = _service.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
