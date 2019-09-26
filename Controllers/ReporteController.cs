using ActualizacionDatosCampaña.Data;
using ActualizacionDatosCampaña.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualizacionDatosCampaña.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index(string vchCodConsultora)
        {
            DatoModel obj = new DatoModel();

            if (!String.IsNullOrEmpty(vchCodConsultora))
            {
                obj.Resultado = DAOConsultora.BusquedaConsultora(vchCodConsultora);
            }


            return View(obj);
        }
      

    }
}