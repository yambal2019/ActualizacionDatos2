using ActualizacionDatosCampaña.Data;
using ActualizacionDatosCampaña.Models;
using ActualizacionDatosCampaña.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ActualizacionDatosCampaña.Controllers
{
    public class DatosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ConsultoraModel modelo)
        {
            DAOConsultora.Busqueda_Consultora(ref modelo);

            if (modelo.Exito == 1)
            {
                Session["Model"] = modelo;

                return RedirectToAction("Consultora", "Datos");
            }
            ViewBag.Message = "Error DNI no Existe..";

            return View();
        }
        [HttpGet]
        public ActionResult Consultora()
        {
            ConsultoraModel objConsultoraModel = (ConsultoraModel)Session["Model"];
            if (objConsultoraModel == null)
            {
                return RedirectToAction("Index", "Datos");
            }


            DatoModel obj = new DatoModel();
            obj.vchNombre = objConsultoraModel.vchNombre;
            obj.idPromocion = objConsultoraModel.idPromocion;

            return View(obj);
        }


        [HttpPost]
        public async Task<ActionResult> Consultora(DatoModel modelo)
        {
            if (modelo.vchTelefono != null && modelo.vchTelefono.Length == 9)
            {
                DatoModel objDatoModel = new DatoModel();
                try
                {

                    //*******************************************************************


                    objDatoModel.idTipoDocumento = 1;
                    objDatoModel.vchDato = modelo.vchDato;
                    // objDatoModel.vchEmail = modelo.vchEmail;
                    objDatoModel.vchTelefono = modelo.vchTelefono;
                    objDatoModel.bitEnviado = true;
                    objDatoModel.dtmFechaEnvio = DateTime.Now;
                    objDatoModel.vchCodConsultora = modelo.vchCodConsultora;
                    objDatoModel.vchEstado = "1";
                    objDatoModel.idPromocion = modelo.idPromocion;
                    DAODato.Add(ref objDatoModel);
                    //*******************************************************************
                    objDatoModel.vchEncriptadoSMS = Encriptacion.Base64Encode(objDatoModel.idDato + "," + "1" + "," + objDatoModel.vchCodConsultora + "," + 1);


                    await Helper.EnvioSMSAsync(modelo);

                    //*******************************************************************

                    return RedirectToAction("Confirmacion", "Datos");
                }


                catch (Exception ex)
                {
                    Log.Error(ex.StackTrace + "-----" + ex.Message);
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Confirmacion()
        {


            return View();
        }



    }
}