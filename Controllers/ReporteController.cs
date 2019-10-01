using ActualizacionDatosCampaña.Data;
using ActualizacionDatosCampaña.Models;
using ActualizacionDatosCampaña.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualizacionDatosCampaña.Controllers
{
    public class ReporteController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {
            DatosViewModel obj = new DatosViewModel();
            obj.ListaTipoDocumento = Helper.ListaTipoDocumento();

            return View(obj);
        }

      

       
       
        [HttpPost]
        public ActionResult Index(DatosViewModel objmodel)
        {
            objmodel.ListaTipoDocumento = Helper.ListaTipoDocumento();

            if (objmodel.TipoDocumentoId == 0)
            {
                ModelState.AddModelError("TipoDocumentoId", "Seleccione el Tipo de Documento");
                return View(objmodel);
            }

            if (objmodel.vchDato.Length == 0)
            {
                ModelState.AddModelError("vchDato", "Ingrese un dato");
                return View(objmodel);
            }

    

            objmodel.Resultado = DAODato.BusquedaConsultora(objmodel);

            return View(objmodel);
        }

    }
}