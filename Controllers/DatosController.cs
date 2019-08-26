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
            if (modelo.bitTerminosCondiciones == true)
            {
                if (modelo.Exito == 1)
                {
                    Session["Model"] = modelo;
                    return RedirectToAction("Consultora", "Datos");
                }
                else
                {
                    ModelState.AddModelError("DNI", "Error DNI no Existe..");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("DNI", "acepte los terminos y condiciones");
                return View();
            }
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
            if (modelo.vchTelefono != null && modelo.vchTelefono.Length == 11)
            {
                DatoModel objDatoModel = new DatoModel();
                try
                {
                    ConsultoraModel objConsultoraModel = (ConsultoraModel)Session["Model"];

                    //*******************************************************************


                    //objDatoModel.idTipoDocumento = 1;
                    //objDatoModel.vchDato = modelo.vchDato;
                    //// objDatoModel.vchEmail = modelo.vchEmail;
                    //objDatoModel.vchTelefono = modelo.vchTelefono;
                    //objDatoModel.bitEnviado = true;
                    //objDatoModel.dtmFechaEnvio = DateTime.Now;
                    //objDatoModel.vchCodConsultora = modelo.vchCodConsultora;
                    //objDatoModel.vchEstado = "1";
                    //objDatoModel.idPromocion = modelo.idPromocion;

                    //objDatoModel.vchDato = modelo.vchDato;
                    objDatoModel.vchEmail = string.Empty;
                    objDatoModel.vchTelefono = modelo.vchTelefono;
                    objDatoModel.bitEnviadoSMS = true;
                    objDatoModel.dtmFechaEnvioSMS = DateTime.Now;

                    objDatoModel.bitConfirmadoSMS = false;
                    // objDatoModel.dtmFechaConfirmadoSMS= DBNull.Value;
                    // objDatoModel.bitEnviadoEmail= modelo.bitEnviadoEmail;
                    //objDatoModel.dtmFechaEnviadoEmail= modelo.dtmFechaEnviadoEmail;
                    //objDatoModel.bitConfirmadoEmail= modelo.bitConfirmadoEmail;
                    // objDatoModel.dtmFechaConfirmadoEmail= modelo.dtmFechaConfirmadoEmail;
                    objDatoModel.vchCodConsultora = objConsultoraModel.vchCodConsultora;
                    objDatoModel.vchEstado = "1";
                    objDatoModel.idPromocion = objConsultoraModel.idPromocion;

                    DAODato.Add(ref objDatoModel);
                    //*******************************************************************
                    objDatoModel.vchEncriptadoSMS = Encriptacion.Base64Encode(objDatoModel.idDato + "," + "1" );


                     await Helper.EnvioSMSAsync(objDatoModel);

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