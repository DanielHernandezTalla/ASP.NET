using Auth.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        [AutorizeUser(idOperacion: 4)]
        public ActionResult Index()
        {
            return View();
        }
        
        [AutorizeUser(idOperacion: 3)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AutorizeUser(idOperacion: 2)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}