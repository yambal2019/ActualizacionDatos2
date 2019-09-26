using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActualizacionDatosCampaña.Models;

namespace ActualizacionDatosCampaña.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autorizacion(UserModel model)
        {
            if (model.User == "1" && model.Password == "1")
            {
                Session["UserId"] = "12";
                Session["UserName"] = model.UserName;

                return RedirectToAction("Index", "Reporte");

            }
            else
            {
                ModelState.AddModelError("xx", "Error en el Usuario o Password");
    
                return View("Index",model);
            }

        }

        public ActionResult CerraSesion()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}