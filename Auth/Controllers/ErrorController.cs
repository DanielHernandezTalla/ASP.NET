using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult UnauthorizedOperation(String operation, String modulo, String mensajeError)
        {
            ViewBag.opeartion = operation;
            ViewBag.modulo = modulo;
            ViewBag.mensajeError = mensajeError;
            return View();
        }
    }
}