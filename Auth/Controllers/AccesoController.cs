using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string pass)
        {
            try
            {
                using (Models.MVC_UdemyEntities db = new Models.MVC_UdemyEntities())
                {
                    var oUsuario = (from d in db.tblUsuarios
                                    where d.email == usuario && d.password == pass
                                    select d).FirstOrDefault();
                    if(oUsuario == null)
                    {
                        ViewBag.Error = "Usuario o contrasena invalida";
                        return View();
                    }
                    // Creamos el usuario para dar acceso
                    Session["User"] = oUsuario;
                    //Para cerrar sesion
                    // Session["User"] = "";
                }

                return RedirectToAction("Index", "Home");
            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }

        }
    }
}