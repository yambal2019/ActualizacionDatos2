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
           
            parametro = parametro.Substring(0, parametro.Length - 1);

            String Clave = Encriptacion.Base64Decode(parametro);
            string[] lista = Clave.Split(',');
            DatoModel objModel = new DatoModel();
            objModel.idDato = Convert.ToInt32(lista[0]);

            if (lista[1] == "2") //Email
            {
                objModel.TipoEnvio = 3;
                objModel.bitConfirmadoEmail = true;
                objModel.dtmFechaConfirmadoEmail = DateTime.Now;
                objModel.vchEstado = "4";

                DAODato.Update(objModel);

            }
            return View("Index");
        }
    }
}