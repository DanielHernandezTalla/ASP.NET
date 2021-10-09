using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutorizeUser: AuthorizeAttribute 
    {
        private tblUsuario oUsuario;
        private MVC_UdemyEntities bd = new MVC_UdemyEntities();
        private int IdOperation;

        public AutorizeUser(int idOperacion = 0)
        {
            this.IdOperation = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperation = "";
            String nombreModulo = "";

            try
            {
                oUsuario = (tblUsuario)HttpContext.Current.Session["User"];

                var misOperaciones = from m in bd.tblRolOperacions
                                     where m.idRol == oUsuario.idRol
                                     && m.idOperacion == IdOperation
                                     select m;
                if (misOperaciones.ToList().Count == 0)
                {
                    var oOperacion = bd.tblOperaciones.Find(IdOperation);
                    int? idModulo = oOperacion.idModulo;
                    nombreOperation = getNombreOperacion(IdOperation);
                    nombreModulo = getNombreOperacion(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperation + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");

                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operation+ " + nombreOperation + nombreModulo);
            }
        }

        private string getNombreModulo(int? idModulo)
        {
            var modulo = from m in bd.tblModuloes
                         where m.id == idModulo
                         select m.nombre;
            String nombreModulo = "";

            try{
                nombreModulo = modulo.First();
            }catch (Exception)
            {
                nombreModulo = "";
            }
            return nombreModulo;
        }

        private string getNombreOperacion(int? idOperacion)
        {
            var operacion = from o in bd.tblOperaciones
                            where o.id == idOperacion
                            select o.nombre;
            string nombreOperacion = "";

            try
            {
                nombreOperacion = operacion.First();
            }
            catch (Exception)
            {
                nombreOperacion = "";
            }
            return nombreOperacion;
        }

    }
}