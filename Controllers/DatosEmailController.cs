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
    public class DatosEmailController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id)
        {
            string parametro = Convert.ToString(id);
            String Clave = Encriptacion.Base64Decode(parametro);
            string[] lista = Clave.Split(',');
            DatoModel objModel = new DatoModel();
            objModel.idDato = Convert.ToInt32(lista[0]);

            //objModel.vchCodConsultora = Convert.ToString(lista[2]);
            //objModel.idPromocion = Convert.ToInt32(lista[3]);

            if (lista[1] == "1") //SMS
            {
                objModel.TipoEnvio = 1;
                objModel.bitConfirmadoSMS = true;
                objModel.dtmFechaConfirmadoSMS = DateTime.Now;
                objModel.vchEstado = "2";

                DAODato.Update(objModel);
                //ViewBag.Texto = Helper.TextoBD("MENSAJE_CONFIRMACION_SMS");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {


            return View("Email");
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