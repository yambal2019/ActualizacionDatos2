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
    public class FinalDatosController : Controller
    {
        // GET: Final de email confirmación
        public ActionResult Index(string id)
        {
            string parametro = Convert.ToString(id);
            String Clave = Encriptacion.Base64Decode(parametro);
            string[] lista = Clave.Split(',');
            DatoModel objModel = new DatoModel();
            objModel.intDato = Convert.ToInt32(lista[0]);

            if (lista[1] == "2") //Email
            {
                objModel.TipoEnvio = 3;
                objModel.bitConfirmadoEmail = true;
                objModel.dtmFechaConfirmadoEmail = DateTime.Now;
                objModel.vchEstado = "4";

                DAODato.Update(objModel);
                
            }
            return View();
        }
    }
}