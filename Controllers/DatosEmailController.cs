using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualizacionDatosCampaña.Controllers
{
    public class DatosEmailController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Email()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Email(FormCollection formCollection)
        {
            return View();
        }
    }
}